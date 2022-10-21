using System.Collections.Generic;
using UnityEngine;

public abstract class HumanAirState : HumanState, ICollisionHandler
{
    protected interface IWingControl
    {
        float WingArea { get; }
        Vector3 WingNormal { get; }
        Vector3 WingPivot { get; }
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

        Vector3 globalNormal = wing.WingNormal;
        Vector3 globalPoint = Human.TransformState.LocalToWorld.MultiplyPoint3x4(wing.WingPivot);

        Vector3 velocity = Human.MoveSystem.GetVelocityAtPoint(globalPoint);

        Vector3 resistanceNormal = globalNormal * -Mathf.Sign(Vector3.Dot(velocity, globalNormal));
        float vDot = -Vector3.Dot(velocity, resistanceNormal);
        vDot = Mathf.Sqrt(vDot);

        Vector3 resistance = resistanceNormal * vDot * windage * wing.WingArea;
        Human.MoveSystem.AddForceAtPoint(resistance, globalPoint);
    }
}
