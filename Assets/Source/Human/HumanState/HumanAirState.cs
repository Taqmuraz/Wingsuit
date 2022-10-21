using System.Collections.Generic;
using UnityEngine;

public abstract class HumanAirState : HumanState, ICollisionHandler
{
    protected interface IWingControl
    {
        float WingOpenAngleNormalized { get; }
        Vector2 WingRotation { get; }
        Vector3 WingPivot { get; }
    }
    protected sealed class StaticWing : IWingControl
    {
        public StaticWing(float wingOpenAngle, Vector2 wingRotation, Vector3 wingPivot)
        {
            WingOpenAngleNormalized = wingOpenAngle;
            WingRotation = wingRotation;
            WingPivot = wingPivot;
        }

        public float WingOpenAngleNormalized { get; }
        public Vector2 WingRotation { get; }
        public Vector3 WingPivot { get; }
    }

    protected sealed class DynamicWingControl : IWingControl
    {
        public float WingOpenAngleNormalized { get; private set; }
        public Vector2 WingRotation { get; private set; }
        readonly float updateSpeed = 3f;

        public DynamicWingControl(Vector3 wingPivot)
        {
            WingPivot = wingPivot;
        }

        public void UpdateOpenAngle(float angle)
        {
            WingOpenAngleNormalized = Mathf.Lerp(WingOpenAngleNormalized, angle, Time.deltaTime * updateSpeed);
        }
        public void UpdateRotation(Vector2 rotation)
        {
            WingRotation = Vector2.Lerp(WingRotation, rotation, Time.deltaTime * updateSpeed);
        }

        public Vector3 WingPivot { get; }
    }

    public void OnCollisionEnter(Vector3 point, Vector3 normal, Vector3 impulse)
    {
        if (normal.y > 0.9f && impulse.magnitude < 10f)
        {
            Human.TransformState.Position = point;
            MoveToState("Default");
        }
        else
        {
            MoveToState("Disabled");
        }
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

    protected abstract IEnumerable<IWingControl> EnumerateControls();

    [BehaviourEvent]
    void FixedUpdate()
    {
        foreach (var control in EnumerateControls()) UpdateWingPhysics(control);
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

        Vector3 resistance = resistanceNormal * vDot * windage * (wing.WingOpenAngleNormalized);
        Human.MoveSystem.AddForceAtPoint(resistance, globalPoint);
    }
}
