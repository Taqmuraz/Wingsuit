public interface IState<TKey> : IEventsHandler
{
    TKey Key { get; }
    TKey GetNextState();
}
