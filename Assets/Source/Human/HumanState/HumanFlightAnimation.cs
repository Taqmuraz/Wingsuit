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

        protected override void Animate()
        {
            Root.Rotate(Quaternion.AngleAxis(90f, Vector3.right));
            Head.Rotate(Quaternion.AngleAxis(-45f, Vector3.right));

            float legAngleBase = 15f;

            LeftLeg.Rotate(Quaternion.Euler(0f, state.backWing.WingRotation, -state.backWing.WingOpenAngle * 0.5f + legAngleBase));
            RightLeg.Rotate(Quaternion.Euler(0f, state.backWing.WingRotation, state.backWing.WingOpenAngle * 0.5f - legAngleBase));

            LeftArm.Rotate(Quaternion.Euler(0f, state.leftWing.WingRotation, -state.leftWing.WingOpenAngle));
            RightArm.Rotate(Quaternion.Euler(0f, state.rightWing.WingRotation, state.rightWing.WingOpenAngle));
        }
    }
}