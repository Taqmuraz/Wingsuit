using System;
using System.Collections.Generic;

public sealed class MoveToStateAction : IControlAction
{
    HashSet<string> fromStates;
    string toState;

    public MoveToStateAction(string toState, params string[] fromStates)
    {
        this.fromStates = new HashSet<string>(fromStates);
        this.toState = toState;
    }

    public void VisitAcceptor(IControlAcceptor acceptor)
    {
       if (fromStates.Contains(acceptor.State)) acceptor.MoveToState(toState);
    }
}
public sealed class StateAttachedAction : IControlAction
{
    string state;
    Action<IControlAcceptor> action;

    public StateAttachedAction(string state, Action<IControlAcceptor> action)
    {
        this.state = state;
        this.action = action;
    }

    public void VisitAcceptor(IControlAcceptor acceptor)
    {
        if (acceptor.State == state) action(acceptor);
    }
}