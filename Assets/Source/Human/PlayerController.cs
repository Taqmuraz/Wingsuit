using System.Collections.Generic;
using UnityEngine;
using Core.UserInterface.UnityUI;
using Core.UserInterface.Basement;
using Core.UserInterface;

public sealed class PlayerController : HumanController, IHumanControlProvider, IControlAction, IFlightInput, IVehicleInput, IUser
{
    sealed class PlayerResetAction : IControlAction
    {
        PlayerController player;

        public PlayerResetAction(PlayerController player)
        {
            this.player = player;
        }

        public void VisitAcceptor(IControlAcceptor acceptor)
        {
            acceptor.MoveToState("Default");
            player.TransformState.Position = player.startPosition;
        }
    }

    public static IHumanController Player { get; private set; }

    public override IHumanControlProvider ControlProvider => this;
    ICameraController cameraController;
    string currentStateKey;
    Camera mainCamera;
    Vector3 startPosition;

    IControlAction resetAction;
    IControlAction flightToParachuteAction = new MoveToStateAction("OpenParachute", "Flight");
    IControlAction parachuteToFlightAction = new MoveToStateAction("Flight", "Parachute");
    IControlAction flightToRock = new MoveToStateAction("Rock", "Flight");
    IControlAction rockToFlight = new MoveToStateAction("Flight", "Rock");
    IExtendedCanvas playerCanvas;
    DrawOperationIdentifier playerCanvasIdentifier = new DrawOperationIdentifier();

    SafeDictionary<string, IPlayerCameraMode> cameraControllerModes;

    protected override IMoveSystem CreateMoveSystem()
    {
        return new RigidbodyMoveSystem(gameObject);
    }

    protected override void Initialize()
    {
        mainCamera = Camera.main;
        cameraController = new PlayerCameraController(this);
        Camera.onPreRender += UpdateCamera;

        var defaultCameraMode = new PlayerStandardCameraControllerMode();
        var flightCameraMode = new PlayerFlightCameraControllerMode(this);

        cameraControllerModes = new SafeDictionary<string, IPlayerCameraMode>(new Dictionary<string, IPlayerCameraMode>()
        {
            ["Flight"] = flightCameraMode,
            ["Vehicle"] = new PlayerVehicleCameraControllerMode(this),
        }, () => defaultCameraMode);

        startPosition = transform.position;
        resetAction = new PlayerResetAction(this);

        Player = this;

        playerCanvas = UnityCanvasExtended.CreateCanvas();
    }
    protected override void OnFinalize()
    {
        Camera.onPreRender -= UpdateCamera;
    }

    void UpdateCamera(Camera camera)
    {
        if (camera == mainCamera)
        {
            cameraController.UpdateCamera(camera, cameraControllerModes[currentStateKey]);
        }
    }

    Matrix4x4 InputMatrix
    {
        get
        {
            Matrix4x4 matrix = mainCamera.transform.localToWorldMatrix;
            Vector3 right = matrix.GetColumn(0);
            Vector3 fwd = matrix.GetColumn(2);
            right = right.WithY(0f).normalized;
            fwd = fwd.WithY(0f).normalized;
            matrix.SetColumn(0, right);
            matrix.SetColumn(2, fwd);
            matrix.SetColumn(1, Vector3.up);
            return matrix;
        }
    }

    public Vector3 InputMovement => InputMatrix.MultiplyVector(InputMovementLocal.normalized);
    Queue<IControlAction> deferredActions = new Queue<IControlAction>();

    static IInputProvider Input => InputProvider.GetInputProvider();

    public IControlAction GetAction()
    {
        return this;
    }
    void EnqueueAction(IControlAction action)
    {
        deferredActions.Enqueue(action);
    }

    public float CommonWingsOpenness { get; private set; }
    public float ForwardWingsOpenness { get; private set; }
    public float BackWingsOpenness { get; private set; }
    public float LeftWingOpenness { get; private set; }
    public float RightWingOpenness { get; private set; }
    public Vector2 LeftWingRotationNormalized { get; private set; }
    public Vector2 RightWingRotationNormalized { get; private set; }
    public Vector2 BackWingRotationNormalized { get; private set; }

    protected override void OnUpdate()
    {
        bool shift = Input.GetKey(KeyCode.LeftShift);
        CommonWingsOpenness = Input.GetKey(KeyCode.Space) ? 0f : (shift ? 1f : 0.5f);

        ForwardWingsOpenness = Input.GetKey(KeyCode.UpArrow) ? 0f : 1f;
        BackWingsOpenness = Input.GetKey(KeyCode.DownArrow) ? 0f : 1f;
        LeftWingOpenness = Input.GetKey(KeyCode.LeftArrow) ? 0f : 1f;
        RightWingOpenness = Input.GetKey(KeyCode.RightArrow) ? 0f : 1f;
        var rotationInput = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        LeftWingRotationNormalized = new Vector2(rotationInput.y, 0f);
        RightWingRotationNormalized = new Vector2(-rotationInput.y, 0f);
        
        BackWingRotationNormalized = new Vector2(rotationInput.x, rotationInput.y);

        if (Input.GetKey(KeyCode.X))
        {
            float additionalAngle = 1f;
            LeftWingRotationNormalized += new Vector2(0f, additionalAngle);
            RightWingRotationNormalized += new Vector2(0f, -additionalAngle);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.LoadLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EnqueueAction(flightToParachuteAction);
            EnqueueAction(parachuteToFlightAction);
        }
        if (Input.GetKey(KeyCode.R))
        {
            EnqueueAction(resetAction);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            EnqueueAction(rockToFlight);
            EnqueueAction(flightToRock);
        }

        playerCanvas.Clear();
        playerCanvas.BeginDraw(playerCanvasIdentifier);

        if (Usable.FindUsable(new Bounds(MoveSystem.Center, MoveSystem.Size), out IUsable usable))
        {
            playerCanvas.DrawText($"Press F to use {usable.Description}", new Rect(Screen.width * 0.5f - 100f, 0f, 200f, 100f));

            if (Input.GetKeyDown(KeyCode.F))
            {
                EnqueueAction(usable.Use(this));
            }
        }

        playerCanvas.EndDraw();
    }

    public Vector3 InputMovementLocal => Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")), 1f);

    void IControlAction.VisitAcceptor(IControlAcceptor acceptor)
    {
        currentStateKey = acceptor.State;

        foreach (var action in deferredActions)
        {
            action.VisitAcceptor(acceptor);
        }
        deferredActions.Clear();
    }

    IFlightInput IHumanControlProvider.InputFlight => this;
    IVehicleInput IHumanControlProvider.VehicleInput => this;

    Vector4 IVehicleInput.ControlInput => new Vector4()
    {
        x = Input.GetAxis("Horizontal"),
        y = Input.GetAxis("Vertical"),
        z = Input.GetKey(KeyCode.Z) ? -1f : (Input.GetKey(KeyCode.C) ? 1f : 0f),
    };
    float IVehicleInput.AccelerationInput => Input.GetKey(KeyCode.LeftShift) ? 1f : 0f;
}
