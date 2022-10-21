using UnityEngine;

public abstract class HumanMovementState : HumanState
{
    protected abstract float MoveSpeed { get; }
    protected abstract string IdleAnimation { get; }
    protected abstract string MoveAnimation { get; }

    [BehaviourEvent]
    void OnEnter()
    {
        Human.TransformState.Rotation = Quaternion.LookRotation(Human.TransformState.Forward.WithY(0f));
    }

    [BehaviourEvent]
    void Update()
    {
        Vector3 input = Human.ControlProvider.InputMovement;
        if (input != new Vector3()) Human.TransformState.Rotation = Quaternion.LookRotation(input, Vector3.up);
        Human.MoveSystem.Move(input * MoveSpeed);
    }
}