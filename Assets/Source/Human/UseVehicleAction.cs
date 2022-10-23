public sealed class UseVehicleAction : IControlAction
{
    public void VisitAcceptor(IControlAcceptor acceptor)
    {
        if (acceptor.State == "Default") acceptor.MoveToState("Vehicle");
    }
}
