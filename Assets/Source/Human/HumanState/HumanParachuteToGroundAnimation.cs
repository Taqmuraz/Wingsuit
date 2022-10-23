using System.Collections.Generic;
using UnityEngine;

public sealed partial class HumanParachuteToGroundState
{
    sealed class HumanParachuteToGroundAnimation : HumanKeyBasedAnimation
    {
        HumanParachuteToGroundState state;

        MemorizedTransformState parachuteState;

        public HumanParachuteToGroundAnimation(IHumanController human, HumanParachuteToGroundState state) : base(human)
        {
            this.state = state;
        }

        [BehaviourEvent]
        void Initialize()
        {
            parachuteState = new MemorizedTransformState(new StandardTransformState(state.parachute.Root));
        }

        [BehaviourEvent]
        void OnExit()
        {
            parachuteState.Reset();
        }

        protected override IEnumerable<HumanBoneAnimation> EnumerataBoneAnimations()
        {
            float moveSpeed = state.Human.MoveSystem.Velocity.WithY(0f).magnitude / state.moveSpeed;
            float timeLength = 1f;
            float legAmplitude = 60f * moveSpeed;

            HumanBoneKey[] legKeys = new HumanBoneKey[]
            {
                new HumanBoneKey(Quaternion.Euler(-legAmplitude, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(-legAmplitude, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(0f, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(legAmplitude * 0.5f, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(legAmplitude, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(legAmplitude * 0.5f, 0f, 0f), Vector3.zero),
            };

            yield return new HumanBoneAnimation(LeftLeg, timeLength, 0f, legKeys);
            yield return new HumanBoneAnimation(RightLeg, timeLength, timeLength * 0.5f, legKeys);

            float kneeAmplitude = 60f * moveSpeed;

            HumanBoneKey[] kneeKeys = new HumanBoneKey[]
            {
                new HumanBoneKey(Quaternion.Euler(-kneeAmplitude, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(0f, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(0f, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(0f, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(-kneeAmplitude * 0.5f, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(-kneeAmplitude, 0f, 0f), Vector3.zero),
            };

            yield return new HumanBoneAnimation(LeftKnee, timeLength, 0f, kneeKeys);
            yield return new HumanBoneAnimation(RightKnee, timeLength, timeLength * 0.5f, kneeKeys);
        }

        protected override void Animate()
        {
            base.Animate();

            float t = (LocalTime / state.timeLength);
            float tInv = Mathf.Clamp01(1f - t * 2f);

            LeftArm.Rotate(Quaternion.Euler(new Vector3(135f, -135f, 180f) * tInv));
            RightArm.Rotate(Quaternion.Euler(new Vector3(135f, 135f, 180f) * tInv));

            LeftForearm.Rotate(Quaternion.Euler(135f * tInv, 0f, 0f));
            RightForearm.Rotate(Quaternion.Euler(135f * tInv, 0f, 0f));

            parachuteState.PreRotate(Quaternion.Euler(-90f * t, 0f, 0f));

            Spine.PreRotate(Quaternion.Euler(-45f * tInv, 0f, 0f));
        }
    }
}
