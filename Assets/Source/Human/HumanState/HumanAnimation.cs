﻿using System.Collections.Generic;
using UnityEngine;

public abstract class HumanAnimation : EventsHandler, IHumanAnimation
{
    protected sealed class HumanBoneState
    {
        public HumanBoneState(ITransformState state)
        {
            InitialLocalPosition = state.LocalPosition;
            InitialLocalRotation = state.LocalRotation;
            State = state;
        }

        public Vector3 InitialLocalPosition { get; }
        public Quaternion InitialLocalRotation { get; }
        public ITransformState State { get; }

        public void Rotate(Quaternion offset)
        {
            State.LocalRotation = InitialLocalRotation * offset;
        }
        public void PreRotate(Quaternion preOffset)
        {
            State.LocalRotation = preOffset * InitialLocalRotation;
        }
        public void Reset()
        {
            State.LocalPosition = InitialLocalPosition;
            State.LocalRotation = InitialLocalRotation;
        }
    }

    protected IHumanController Human { get; }
    protected HumanBoneState LeftLeg { get; }
    protected HumanBoneState RightLeg { get; }
    protected HumanBoneState LeftArm { get; }
    protected HumanBoneState RightArm { get; }
    protected HumanBoneState LeftForearm { get; }
    protected HumanBoneState RightForearm { get; }
    protected HumanBoneState Spine { get; }
    protected HumanBoneState Root { get; }
    protected HumanBoneState Head { get; }
    protected HumanBoneState LeftKnee { get; }
    protected HumanBoneState RightKnee { get; }

    protected float EnterTime { get; private set; }
    protected float LocalTime => Time.time - EnterTime;

    List<HumanBoneState> states = new List<HumanBoneState>();

    [BehaviourEvent]
    void OnEnter()
    {
        EnterTime = Time.time;
        foreach (var state in states) state.Reset();
    }

    HumanBoneState AddBoneState(string boneName)
    {
        var state = new HumanBoneState(Human.GetBone(boneName));
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