public sealed class EmptyAction : IControlAction
{
    public static EmptyAction Instance { get; } = new EmptyAction();

    public void VisitAcceptor(IControlAcceptor acceptor)
    {

    }
}
