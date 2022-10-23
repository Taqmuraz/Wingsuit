using UnityEngine;

public sealed partial class HumanParachuteToGroundState : HumanState
{
    float moveSpeed = 4f;
    float timeLength = 1f;
    Timer exitTimer;
    HumanParachuteEquipmentElement parachute;

    [BehaviourEvent]
    void Initialize()
    {
        parachute = Human.GetEquipmentElement<HumanParachuteEquipmentElement>();
    }

    [BehaviourEvent]
    void Update()
    {
        Human.MoveSystem.Move(Human.TransformState.Forward * moveSpeed);
        if (exitTimer.IsOver) MoveToState("Default");
    }

    [BehaviourEvent]
    void OnEnter()
    {
        parachute.Enable();
        exitTimer = new Timer(timeLength);

        Human.TransformState.Rotation = Quaternion.LookRotation(Human.TransformState.Forward.WithY(0f));
    }

    [BehaviourEvent]
    void OnExit()
    {
        parachute.Disable();
    }

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanParachuteToGroundAnimation(Human, this);
    }
}
