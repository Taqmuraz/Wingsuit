using System.Collections.Generic;
using UnityEngine;

public abstract class HumanAnimation : EventsHandler, IHumanAnimation
{
    protected IHumanController Human { get; }
    protected MemorizedTransformState LeftLeg { get; }
    protected MemorizedTransformState RightLeg { get; }
    protected MemorizedTransformState LeftArm { get; }
    protected MemorizedTransformState RightArm { get; }
    protected MemorizedTransformState LeftForearm { get; }
    protected MemorizedTransformState RightForearm { get; }
    protected MemorizedTransformState Spine { get; }
    protected MemorizedTransformState Root { get; }
    protected MemorizedTransformState Head { get; }
    protected MemorizedTransformState LeftKnee { get; }
    protected MemorizedTransformState RightKnee { get; }

    protected float EnterTime { get; private set; }
    protected float LocalTime => Time.time - EnterTime;

    List<MemorizedTransformState> states = new List<MemorizedTransformState>();

    [BehaviourEvent]
    void OnEnter()
    {
        EnterTime = Time.time;
        if (ResetStateOnEnter) foreach (var state in states) state.Reset();
    }

    protected virtual bool ResetStateOnEnter => true;

    MemorizedTransformState AddBoneState(string boneName)
    {
        var state = new MemorizedTransformState(Human.GetBone(boneName));
        states.Add(state);
        return state;
    }

    public HumanAnimation(IHumanController human)
    {
        this.Human = human;
        LeftLeg = AddBoneState("LeftLeg");
        RightLeg = AddBoneState("RightLeg");
        LeftArm = AddBoneState("LeftArm");
        RightArm = AddBoneState("RightArm");
        LeftForearm = AddBoneState("LeftForearm");
        RightForearm = AddBoneState("RightForearm");
        Spine = AddBoneState("Spine");
        Root = AddBoneState("Root");
        Head = AddBoneState("Head");
        LeftKnee = AddBoneState("LeftKnee");
        RightKnee = AddBoneState("RightKnee");
    }

    [BehaviourEvent]
    void Update()
    {
        Animate();
    }
    protected abstract void Animate();
}