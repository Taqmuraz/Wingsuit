using UnityEngine;

public sealed partial class HumanFlightState : HumanState, ICollisionHandler
{
    sealed class WingControl
    {
        public float WingOpenAngle { get; private set; }
        public float WingRotation { get; private set; }
        readonly float updateSpeed = 3f;

        public void UpdateOpenAngle(float angle)
        {
            WingOpenAngle = Mathf.Lerp(WingOpenAngle, angle, Time.deltaTime * updateSpeed);
        }
        public void UpdateRotation(float rotation)
        {
            WingRotation = Mathf.Lerp(WingRotation, rotation, Time.deltaTime * updateSpeed);
        }
    }
    WingControl leftWing = new WingControl();
    WingControl rightWing = new WingControl();
    WingControl backWing = new WingControl();

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanFlightAnimation(Human, this);
    }

    readonly float wingRotation = 15f;
    readonly float wingOpenAngle = 60f;

    [BehaviourEvent]
    void Update()
    {
        var input = Human.ControlProvider.InputFlight;

        leftWing.UpdateOpenAngle(input.LeftWingOpenness * input.ForwardWingsOpenness * input.CommonWingsOpenness * wingOpenAngle * 0.5f);
        leftWing.UpdateRotation(input.ForwardWingRotationNormalized * wingRotation);

        rightWing.UpdateOpenAngle(input.RightWingOpenness * input.ForwardWingsOpenness * input.CommonWingsOpenness * wingOpenAngle * 0.5f);
        rightWing.UpdateRotation(input.ForwardWingRotationNormalized * wingRotation);

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
        UpdateWingPhysics(leftWing, new Vector3(-0.5f, 0f, 0.5f));
        UpdateWingPhysics(rightWing, new Vector3(0.5f, 0f, 0.5f));
        UpdateWingPhysics(backWing, new Vector3(0f, 0f, -0.5f));
    }

    void UpdateWingPhysics(WingControl wing, Vector3 relativePosition)
    {
        float windage = 0.1f;

        Vector3 localNormal = new Vector3(Mathf.Sin(wing.WingRotation * Mathf.Deg2Rad), Mathf.Cos(wing.WingRotation * Mathf.Deg2Rad), 0f);
        Vector3 globalNormal = Human.TransformState.Rotation * localNormal.normalized;
        Vector3 globalPoint = Human.TransformState.LocalToWorld.MultiplyPoint3x4(relativePosition);

        Vector3 velocity = Human.MoveSystem.GetVelocityAtPoint(globalPoint);

        Vector3 resistanceNormal = globalNormal * -Mathf.Sign(Vector3.Dot(velocity, globalNormal));
        float vDot = -Vector3.Dot(velocity, resistanceNormal);

        Vector3 resistance = resistanceNormal * vDot * windage * (wing.WingOpenAngle / wingOpenAngle);
        Human.MoveSystem.AddForceAtPoint(resistance, globalPoint);
    }

    public void OnCollisionEnter(Vector3 point, Vector3 normal, Vector3 impulse)
    {
        MoveToState("Disabled");
    }
}