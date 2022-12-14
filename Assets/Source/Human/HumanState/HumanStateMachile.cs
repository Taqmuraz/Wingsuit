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
        yield return new HumanDisabledState().Initialize(human);
        yield return new HumanParachuteState().Initialize(human);
        yield return new HumanOpenParachuteState().Initialize(human);
        yield return new HumanRockState().Initialize(human);
        yield return new HumanParachuteToGroundState().Initialize(human);
        yield return new HumanVehicleState().Initialize(human);
    }
}
