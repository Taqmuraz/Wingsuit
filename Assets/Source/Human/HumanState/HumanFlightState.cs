using System.Collections.Generic;
using UnityEngine;

public sealed partial class HumanFlightState : HumanAirState
{
    sealed class StaticWing : IWingControl
    {
        HumanFlightState state;
        Vector2 wingRotation;

        public StaticWing(float wingOpenAngle, Vector2 wingRotation, Vector3 wingPivot, HumanFlightState state)
        {
            this.state = state;
            WingArea = wingOpenAngle;
            this.wingRotation = wingRotation;
            WingPivot = wingPivot + new Vector3(0f, state.HumanSize.y * 0.5f, 0f);
        }

        public float WingArea { get; }
        public Vector3 WingPivot { get; }
        Vector3 WingNormal => state.WingRotationToNormal(wingRotation);

        bool IWingControl.GetResistanceNormal(Vector3 velocity, out Vector3 normal)
        {
            normal = state.ResistanceNormal(WingNormal, velocity);
            return true;
        }
    }

    sealed class DynamicWingControl : IWingControl
    {
        HumanFlightState state;

        public float WingOpenness { get; private set; }
        public Vector2 WingRotation { get; private set; }
        readonly float updateSpeed = 3f;

        public DynamicWingControl(Vector3 wingPivot, HumanFlightState state)
        {
            this.state = state;
            WingPivot = wingPivot + new Vector3(0f, state.HumanSize.y * 0.5f, 0f);
        }

        public void UpdateOpenAngle(float angle)
        {
            WingOpenness = Mathf.Lerp(WingOpenness, angle, Time.deltaTime * updateSpeed);
        }
        public void UpdateRotation(Vector2 rotation)
        {
            WingRotation = Vector2.Lerp(WingRotation, rotation, Time.deltaTime * updateSpeed);
        }

        public Vector3 WingPivot { get; }
        Vector3 WingNormal => state.WingRotationToNormal(WingRotation);

        float IWingControl.WingArea => WingOpenness * 2f;

        bool IWingControl.GetResistanceNormal(Vector3 velocity, out Vector3 normal)
        {
            normal = state.ResistanceNormal(WingNormal, velocity);
            return true;
        }
    }

    Vector3 ResistanceNormal(Vector3 globalNormal, Vector3 velocity)
    {
        return globalNormal * -Mathf.Sign(Vector3.Dot(velocity, globalNormal));
    }

    Vector3 WingRotationToNormal(Vector2 rotation)
    {
        Vector3 localNormal = Quaternion.Euler(-rotation.x, 0f, -rotation.y) * Vector3.up;
        return Human.TransformState.Rotation * localNormal;
    }

    [BehaviourEvent]
    void Initialize()
    {
        leftWing = new DynamicWingControl(new Vector3(-0.15f, 0f, 0.15f), this);
        rightWing = new DynamicWingControl(new Vector3(0.15f, 0f, 0.15f), this);
        backWing = new DynamicWingControl(new Vector3(0f, 0f, -0.25f), this);

        bodySide = new StaticWing(1f, new Vector2(0f, 90f), new Vector3(0f, 0f, -0.2f), this);
        bodyHorizontal = new StaticWing(1f, new Vector2(0f, 0f), new Vector3(0f, 0f, -0.2f), this);
    }

    DynamicWingControl leftWing;
    DynamicWingControl rightWing;
    DynamicWingControl backWing;

    IWingControl bodySide;
    IWingControl bodyHorizontal;

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanFlightAnimation(Human, this);
    }

    const float wingRotation = 30f;

    [BehaviourEvent]
    void Update()
    {
        var input = Human.ControlProvider.InputFlight;

        leftWing.UpdateOpenAngle(input.LeftWingOpenness * input.ForwardWingsOpenness * input.CommonWingsOpenness * 0.5f);
        leftWing.UpdateRotation(input.LeftWingRotationNormalized * wingRotation);

        rightWing.UpdateOpenAngle(input.RightWingOpenness * input.ForwardWingsOpenness * input.CommonWingsOpenness * 0.5f);
        rightWing.UpdateRotation(input.RightWingRotationNormalized * wingRotation);

        backWing.UpdateOpenAngle(input.BackWingsOpenness * input.CommonWingsOpenness);
        backWing.UpdateRotation(input.BackWingRotationNormalized * wingRotation);
    }

    protected override IEnumerable<IWingControl> EnumerateControls()
    {
        yield return leftWing;
        yield return rightWing;
        yield return backWing;
        yield return bodyHorizontal;
        yield return bodySide;
    }
}