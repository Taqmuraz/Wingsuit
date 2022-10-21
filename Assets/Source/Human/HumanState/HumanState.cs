
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanState : EventsHandler, IControlAcceptor, IState<string>
{
    string nextState;
    IHumanAnimation animation;

    public HumanState()
    {
        animation = CreateAnimation();
    }

    protected abstract IHumanAnimation CreateAnimation();

    public virtual Vector3 HumanSize => new Vector3(1f, 1.8f, 1f);

    public string Key { get; private set; }
    protected string DiscardKey => "Discard";

    protected virtual string CreateName() => GetType().Name.Replace("Human", string.Empty).Replace("State", string.Empty);
    protected IHumanController Human { get; private set; }

    public HumanState Initialize(IHumanController controlProvider)
    {
        Key = CreateName();
        Human = controlProvider;
        return this;
    }

    [BehaviourEvent]
    void Update()
    {
        Human.ControlProvider.GetAction().VisitAcceptor(this);
        Human.MoveSystem.Size = HumanSize;
    }

    [BehaviourEvent]
    void OnEnter()
    {
        nextState = Key;
    }

    string IState<string>.GetNextState()
    {
        var next = GetNextState();
        return next == DiscardKey ? nextState : next;
    }
    protected virtual string GetNextState() { return DiscardKey; }

    public void MoveToState(string state)
    {
        nextState = state;
    }

    protected override void OnEventCall(string name)
    {
        animation.CallEvent(name);
    }

    string IControlAcceptor.State => Key;
}
