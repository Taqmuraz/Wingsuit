public interface IControlAcceptor
{
    void MoveToState(string state);
    string State { get; }
}
