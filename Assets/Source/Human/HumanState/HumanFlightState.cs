using System.Collections.Generic;
using UnityEngine;

public sealed partial class HumanFlightState : HumanAirState
{
    DynamicWingControl leftWing = new DynamicWingControl(new Vector3(-0.25f, 0f, 0.25f));
    DynamicWingControl rightWing = new DynamicWingControl(new Vector3(0.25f, 0f, 0.25f));
    DynamicWingControl backWing = new DynamicWingControl(new Vector3(0f, 0f, -0.25f));

    IWingControl bodySide = new StaticWing(1f, new Vector2(0f, 90f), new Vector3(0f, 0f, -0.25f));
    IWingControl bodyHorizontal = new StaticWing(1f, new Vector2(0f, 0f), new Vector3(0f, 0f, -0.25f));

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
        leftWing.UpdateRotation(input.ForwardWingRotationNormalized * wingRotation);

        rightWing.UpdateOpenAngle(input.RightWingOpenness * input.ForwardWingsOpenness * input.CommonWingsOpenness * 0.5f);
        rightWing.UpdateRotation(-input.ForwardWingRotationNormalized * wingRotation);

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