using UnityEngine;

public sealed partial class HumanFlightState : HumanState
{
    sealed class WingControl
    {
        public float WingOpenAngle { get; set; }
        public float WingRotation { get; set; }
    }
    WingControl leftWing = new WingControl();
    WingControl rightWing = new WingControl();
    WingControl backWing = new WingControl();

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanFlightAnimation(Human, this);
    }

    [BehaviourEvent]
    void Update()
    {
        var input = Human.ControlProvider.InputFlight;
        float wingRotation = 45f;
        float wingOpenAngle = 60f;

        leftWing.WingOpenAngle = input.LeftWingOpenness * input.ForwardWingsOpenness * input.CommonWingsOpenness * wingOpenAngle;
        leftWing.WingRotation = input.ForwardWingRotationNormalized * wingRotation;

        rightWing.WingOpenAngle = input.RightWingOpenness * input.ForwardWingsOpenness * input.CommonWingsOpenness * wingOpenAngle;
        rightWing.WingRotation = input.ForwardWingRotationNormalized * wingRotation;

        backWing.WingOpenAngle = input.BackWingsOpenness * input.CommonWingsOpenness * wingOpenAngle;
        backWing.WingRotation = input.BackWingRotationNormalized * wingRotation;
    }

    [BehaviourEvent]
    void OnEnter()
    {
        Human.MoveSystem.EnableFreeRotation = true;
    }
    [BehaviourEvent]
    void OnExit()
    {
        Human.MoveSystem.EnableFreeRotation = false;
    }

    [BehaviourEvent]
    void FixedUpdate()
    {

    }
}