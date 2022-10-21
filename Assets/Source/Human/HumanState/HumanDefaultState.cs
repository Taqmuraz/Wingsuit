public sealed partial class HumanDefaultState : HumanMovementState
{
    protected override float MoveSpeed => 5f;
    protected override string IdleAnimation => "Idle";
    protected override string MoveAnimation => "Run";
    Timer outOfGroundTimer = new Timer(0.2f);

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanWalkAnimation(Human, this);
    }

    [BehaviourEvent]
    void OnEnter()
    {
        outOfGroundTimer = new Timer(0.5f);
    }
    
    [BehaviourEvent]
    void Update()
    {
        if (!Human.MoveSystem.IsOnGround && outOfGroundTimer.IsOver)
        {
            MoveToState("Flight");
        }
    }
}
