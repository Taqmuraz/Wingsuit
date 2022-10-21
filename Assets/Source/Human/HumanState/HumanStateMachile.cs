using System.Collections.Generic;

public sealed class HumanStateMachile : StateMachine<string, HumanState, IHumanController>
{
    IHumanController human;

    public HumanStateMachile(IHumanController human) : base(human)
    {
    }
    protected override void Initialize(IHumanController arg) => human = arg;

    protected override IEnumerable<HumanState> CreateStates()
    {
        yield return new HumanDefaultState().Initialize(human);
        yield return new HumanFlightState().Initialize(human);
    }
}
