using System.Collections.Generic;
using UnityEngine;

partial class HumanDefaultState
{
    public sealed class HumanWalkAnimation : HumanKeyBasedAnimation
    {
        HumanDefaultState state;

        public HumanWalkAnimation(IHumanController human, HumanDefaultState state) : base(human)
        {
            this.state = state;
        }
        protected override IEnumerable<HumanBoneAnimation> EnumerataBoneAnimations()
        {
            float moveSpeed = state.Human.MoveSystem.Velocity.WithY(0f).magnitude / state.MoveSpeed;
            float timeLength = 1f;
            float legAmplitude = 45f * moveSpeed;

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

            float kneeAmplitude = 45f * moveSpeed;

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

            float armAmplitude = 45f * moveSpeed;

            HumanBoneKey[] armKeys = new HumanBoneKey[]
            {
                new HumanBoneKey(Quaternion.Euler(armAmplitude, 0f, 0f), Vector3.zero),
                new HumanBoneKey(Quaternion.Euler(-armAmplitude, 0f, 0f), Vector3.zero),
            };

            yield return new HumanBoneAnimation(LeftArm, timeLength, 0f, armKeys);
            yield return new HumanBoneAnimation(RightArm, timeLength, timeLength * 0.5f, armKeys);
        }
    }
}