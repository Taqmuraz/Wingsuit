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

            LeftLeg.Rotate(Quaternion.Euler(state.backWing.WingRotation.x, state.backWing.WingRotation.y, -state.backWing.WingOpenAngle * 0.5f + legAngleBase));
            RightLeg.Rotate(Quaternion.Euler(state.backWing.WingRotation.x, state.backWing.WingRotation.y, state.backWing.WingOpenAngle * 0.5f - legAngleBase));

            LeftArm.Rotate(Quaternion.Euler(state.leftWing.WingRotation.x, state.leftWing.WingRotation.y, -state.leftWing.WingOpenAngle));
            RightArm.Rotate(Quaternion.Euler(state.rightWing.WingRotation.x, state.rightWing.WingRotation.y, state.rightWing.WingOpenAngle));
        }
    }
}