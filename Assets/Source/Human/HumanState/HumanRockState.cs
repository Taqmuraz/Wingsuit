using System.Collections.Generic;
using UnityEngine;

public sealed class HumanRockState : HumanAirState
{
    sealed class HumanRockAnimation : HumanAnimation
    {
        HumanRockState state;

        public HumanRockAnimation(IHumanController human, HumanRockState state) : base(human)
        {
            this.state = state;
        }

        protected override bool ResetStateOnEnter => false;

        protected override void Animate()
        {
            Head.PreRotateSmooth(Quaternion.Euler(30f, 0f, 0f));
            Root.RotateSmooth(Quaternion.Euler(90f, 0f, 0f));
            Root.State.LocalPosition = new Vector3(0f, state.HumanSize.y * 0.5f, 0f);

            LeftLeg.PreRotateSmooth(Quaternion.Euler(-135f, 0f, 0f));
            RightLeg.PreRotateSmooth(Quaternion.Euler(-135f, 0f, 0f));

            LeftKnee.PreRotateSmooth(Quaternion.Euler(-135f, 0f, 0f));
            RightKnee.PreRotateSmooth(Quaternion.Euler(-135f, 0f, 0f));

            LeftArm.PreRotateSmooth(Quaternion.Euler(-15f, -180f, 15f));
            RightArm.PreRotateSmooth(Quaternion.Euler(-15f, 180f, -15f));

            LeftForearm.PreRotateSmooth(Quaternion.Euler(-90f, 0f, 0f));
            RightForearm.PreRotateSmooth(Quaternion.Euler(-90f, 0f, 0f));
        }
    }

    protected override IEnumerable<IWingControl> EnumerateControls()
    {
        yield break;
    }

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanRockAnimation(Human, this);
    }
}
