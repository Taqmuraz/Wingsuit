using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : HumanController, IHumanControlProvider, IControlAction
{
    public override IHumanControlProvider ControlProvider => this;
    ICameraController cameraController;
    string currentStateKey;
    Camera mainCamera;

    SafeDictionary<string, IPlayerCameraMode> cameraControllerModes;

    protected override IMoveSystem CreateMoveSystem()
    {
        return new RigidbodyMoveSystem(gameObject.AddComponent<Rigidbody>());
    }

    protected override void Initialize()
    {
        mainCamera = Camera.main;
        cameraController = new PlayerCameraController(this);
        Camera.onPreRender += UpdateCamera;

        var defaultCameraMode = new PlayerStandardCameraControllerMode();
        var flightCameraMode = new PlayerFlightCameraControllerMode();

        cameraControllerModes = new SafeDictionary<string, IPlayerCameraMode>(new Dictionary<string, IPlayerCameraMode>()
        {
            ["Flight"] = flightCameraMode,
        }, () => defaultCameraMode);
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

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.LoadLevel(0);
        }
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
}
