using System.Linq;
using UnityEngine;

public sealed partial class HumanParachuteState
{
    public sealed class HumanParachuteAnimation : HumanAnimation
    {
        HumanParachuteState state;

        HumanBoneState[] stropes;

        public HumanParachuteAnimation(IHumanController human, HumanParachuteState state) : base(human)
        {
            this.state = state;
        }

        [BehaviourEvent]
        void Initialize()
        {
            stropes = state.parachute.Stropes.Select(s => new HumanBoneState(new StandardTransformState(s))).ToArray();
        }

        protected override void Animate()
        {
            foreach (var strope in stropes)
            {
                strope.State.LocalScale = Vector3.Lerp(Vector3.zero, strope.InitialLocalScale, LocalTime / parachuteOpenTimeLength);
            }
            LeftArm.Rotate(Quaternion.Euler(135f, -135f, 180f));
            RightArm.Rotate(Quaternion.Euler(135f, 135f, 180f));

            LeftForearm.Rotate(Quaternion.Euler(135f, 0f, 0f));
            RightForearm.Rotate(Quaternion.Euler(135f, 0f, 0f));

            LeftLeg.Rotate(Quaternion.Euler(-15f, 0f, 0f));
            RightLeg.Rotate(Quaternion.Euler(-15f, 0f, 0f));
            LeftKnee.Rotate(Quaternion.Euler(-15f, 0f, 0f));
            RightKnee.Rotate(Quaternion.Euler(-15f, 0f, 0f));
        }
    }
}
