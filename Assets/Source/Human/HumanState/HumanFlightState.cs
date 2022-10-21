using UnityEngine;

public sealed partial class HumanFlightState : HumanState, ICollisionHandler
{
    interface IWingControl
    {
        float WingOpenAngle { get;  }
        Vector2 WingRotation { get; }
        Vector3 WingPivot { get; }
    }
    sealed class StaticWing : IWingControl
    {
        public StaticWing(float wingOpenAngle, Vector2 wingRotation, Vector3 wingPivot)
        {
            WingOpenAngle = wingOpenAngle;
            WingRotation = wingRotation;
            WingPivot = wingPivot;
        }

        public float WingOpenAngle { get; }
        public Vector2 WingRotation { get; }
        public Vector3 WingPivot { get; }
    }

    sealed class DynamicWingControl : IWingControl
    {
        public float WingOpenAngle { get; private set; }
        public Vector2 WingRotation { get; private set; }
        readonly float updateSpeed = 3f;

        public DynamicWingControl(Vector3 wingPivot)
        {
            WingPivot = wingPivot;
        }

        public void UpdateOpenAngle(float angle)
        {
            WingOpenAngle = Mathf.Lerp(WingOpenAngle, angle, Time.deltaTime * updateSpeed);
        }
        public void UpdateRotation(Vector2 rotation)
        {
            WingRotation = Vector2.Lerp(WingRotation, rotation, Time.deltaTime * updateSpeed);
        }

        public Vector3 WingPivot { get; }
    }
    DynamicWingControl leftWing = new DynamicWingControl(new Vector3(-0.25f, 0f, 0.25f));
    DynamicWingControl rightWing = new DynamicWingControl(new Vector3(0.25f, 0f, 0.25f));
    DynamicWingControl backWing = new DynamicWingControl(new Vector3(0f, 0f, -0.25f));

    IWingControl bodySide = new StaticWing(wingOpenAngle * 0.5f, new Vector2(0f, 90f), new Vector3(0f, 0f, -0.25f));
    IWingControl bodyHorizontal = new StaticWing(wingOpenAngle * 0.5f, new Vector2(0f, 0f), new Vector3(0f, 0f, -0.25f));

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanFlightAnimation(Human, this);
    }

    const float wingRotation = 30f;
    const float wingOpenAngle = 60f;

    [BehaviourEvent]
    void Update()
    {
        var input = Human.ControlProvider.InputFlight;

        leftWing.UpdateOpenAngle(input.LeftWingOpenness * input.ForwardWingsOpenness * input.CommonWingsOpenness * wingOpenAngle * 0.5f);
        leftWing.UpdateRotation(input.ForwardWingRotationNormalized * wingRotation);

        rightWing.UpdateOpenAngle(input.RightWingOpenness * input.ForwardWingsOpenness * input.CommonWingsOpenness * wingOpenAngle * 0.5f);
        rightWing.UpdateRotation(-input.ForwardWingRotationNormalized * wingRotation);

        backWing.UpdateOpenAngle(input.BackWingsOpenness * input.CommonWingsOpenness * wingOpenAngle);
        backWing.UpdateRotation(input.BackWingRotationNormalized * wingRotation);
    }

    [BehaviourEvent]
    void OnEnter()
    {
        Human.MoveSystem.EnableFreeRotation = true;
        Human.SubscribeCollisionHandler(this);
    }
    [BehaviourEvent]
    void OnExit()
    {
        Human.MoveSystem.EnableFreeRotation = false;
        Human.UnsubscribeCollisionHandler(this);
    }

    [BehaviourEvent]
    void FixedUpdate()
    {
        UpdateWingPhysics(leftWing);
        UpdateWingPhysics(rightWing);
        UpdateWingPhysics(backWing);
        UpdateWingPhysics(bodySide);
        UpdateWingPhysics(bodyHorizontal);
    }

    void UpdateWingPhysics(IWingControl wing)
    {
        float windage = 6f;

        Vector3 localNormal = Quaternion.Euler(-wing.WingRotation.x, 0f, -wing.WingRotation.y) * Vector3.up;

        Vector3 globalNormal = Human.TransformState.Rotation * localNormal.normalized;
        Vector3 globalPoint = Human.TransformState.LocalToWorld.MultiplyPoint3x4(wing.WingPivot + new Vector3(0f, HumanSize.y * 0.5f, 0f));

        Vector3 velocity = Human.MoveSystem.GetVelocityAtPoint(globalPoint);

        Vector3 resistanceNormal = globalNormal * -Mathf.Sign(Vector3.Dot(velocity, globalNormal));
        float vDot = -Vector3.Dot(velocity, resistanceNormal);
        vDot = Mathf.Sqrt(vDot);

        Vector3 resistance = resistanceNormal * vDot * windage * (wing.WingOpenAngle / wingOpenAngle);
        Human.MoveSystem.AddForceAtPoint(resistance, globalPoint);
    }

    public void OnCollisionEnter(Vector3 point, Vector3 normal, Vector3 impulse)
    {
        MoveToState("Disabled");
    }
}