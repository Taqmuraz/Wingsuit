using UnityEngine;

public sealed partial class HumanFlightState
{
    public sealed class HumanFlightAnimation : HumanAnimation
    {
        HumanFlightState state;

        public HumanFlightAnimation(IHumanController human, HumanFlightState state) : base(human)
        {
            this.state = state;
        }

        const float wingOpenAngle = 60f;

        protected override void Animate()
        {
            float cosmeticRotationMultiplier = 1f;

            Root.Rotate(Quaternion.AngleAxis(90f, Vector3.right));
            Head.Rotate(Quaternion.AngleAxis(-45f, Vector3.right));

            float legAngleBase = 15f;
            float kneeAngleBase = 15f;

            Vector2 backWingRotation = state.backWing.WingRotation * cosmeticRotationMultiplier;
            Root.State.LocalPosition = new Vector3(0f, state.HumanSize.y * 0.5f, 0f);

            LeftLeg.Rotate(Quaternion.Euler(backWingRotation.x * 0.5f, backWingRotation.y * 0.5f, -state.backWing.WingOpenness * wingOpenAngle * 0.5f + legAngleBase));
            RightLeg.Rotate(Quaternion.Euler(backWingRotation.x * 0.5f, backWingRotation.y * 0.5f, state.backWing.WingOpenness * wingOpenAngle * 0.5f - legAngleBase));

            LeftKnee.Rotate(Quaternion.Euler(backWingRotation.x * 0.5f - kneeAngleBase, 0f, 0f));
            RightKnee.Rotate(Quaternion.Euler(backWingRotation.x * 0.5f - kneeAngleBase, 0f, 0f));

            LeftArm.Rotate(Quaternion.Euler(state.leftWing.WingRotation.x * cosmeticRotationMultiplier, state.leftWing.WingRotation.y * cosmeticRotationMultiplier, -state.leftWing.WingOpenness * wingOpenAngle));
            RightArm.Rotate(Quaternion.Euler(state.rightWing.WingRotation.x * cosmeticRotationMultiplier, state.rightWing.WingRotation.y * cosmeticRotationMultiplier, state.rightWing.WingOpenness * wingOpenAngle));
        }
    }
}