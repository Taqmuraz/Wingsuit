using System.Collections.Generic;
using System.Linq;

public abstract class StateMachine<TKey, TState, TArg> : EventsHandler where TState : IState<TKey>
{
    Dictionary<TKey, TState> statesMap = new Dictionary<TKey, TState>();
    TState activeState;

    public StateMachine(TArg arg)
    {
        Initialize(arg);
        statesMap = CreateStates().ToDictionary(s => s.Key);
        activeState = statesMap.Values.First();
        activeState.CallEvent("OnEnter");
    }

    protected abstract void Initialize(TArg arg);

    protected abstract IEnumerable<TState> CreateStates();

    [BehaviourEvent]
    void Update()
    {
        var nextStateKey = activeState.GetNextState();
        if (!nextStateKey.Equals(activeState.Key))
        {
            activeState.CallEvent("OnExit");
            activeState = statesMap[nextStateKey];
            activeState.CallEvent("OnEnter");
        }
    }

    protected override void OnEventCall(string name)
    {
        activeState.CallEvent(name);
    }
}
