public sealed class HumanDefaultState : HumanMovementState
{
    protected override float MoveSpeed => 5f;
    protected override string IdleAnimation => "Idle";
    protected override string MoveAnimation => "Run";

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanWalkAnimation(Human);
    }
}
