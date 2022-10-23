using System.Collections.Generic;
using UnityEngine;

public abstract class HumanAirState : HumanState, ICollisionHandler
{
    public void OnCollisionEnter(Vector3 point, Vector3 normal, Vector3 impulse)
    {
        if (Human.MoveSystem.IsOnGround && impulse.magnitude < LandingMaxVelocity)
        {
            OnLanded(point, normal, impulse);
        }
        else
        {
            MoveToState("Disabled");
        }
    }

    protected virtual float LandingMaxVelocity => 10f;

    protected virtual void OnLanded(Vector3 point, Vector3 normal, Vector3 impulse)
    {
        Human.TransformState.Position = point;
        MoveToState("Default");
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

    protected virtual IAirForceEngine AirForceEngine { get; } = new DefaultAirForceEngine();

    [BehaviourEvent]
    void FixedUpdate()
    {
        foreach (var control in EnumerateControls()) UpdateWingPhysics(control);
    }

    void UpdateWingPhysics(IWingControl wing)
    {
        AirForceEngine.CalculateAirForce(wing, Human.MoveSystem, Human.TransformState);
    }
}
