using System.Collections.Generic;
using UnityEngine;

public abstract class HumanKeyBasedAnimation : HumanAnimation
{
    protected HumanKeyBasedAnimation(IHumanController human) : base(human)
    {
    }

    protected sealed class HumanBoneAnimation
    {
        HumanBoneKey[] keys;
        MemorizedTransformState bone;
        float timeLength;
        float timeOffset;

        public HumanBoneAnimation(MemorizedTransformState bone, float timeLength, float timeOffset, params HumanBoneKey[] keys)
        {
            this.bone = bone;
            this.timeLength = timeLength;
            this.keys = keys;
            this.timeOffset = timeOffset;
        }

        public void Apply(float time)
        {
            time += timeOffset;

            float localTime = time - (int)(time / timeLength) * timeLength;
            int startKey = ((int)((localTime / timeLength) * keys.Length)) % keys.Length;
            int endKey = (startKey + 1) % keys.Length;
            float keyLength = timeLength / keys.Length;

            float keyTime = (localTime - (keyLength * startKey)) / keyLength;
            HumanBoneKey.Lerp(keys[startKey], keys[endKey], keyTime).Apply(bone);
        }
    }

    protected struct HumanBoneKey
    {
        Quaternion localRotation;
        Vector3 localPosition;

        public HumanBoneKey(Quaternion localRotation, Vector3 localPosition)
        {
            this.localRotation = localRotation;
            this.localPosition = localPosition;
        }

        public void Apply(MemorizedTransformState state)
        {
            state.PreRotate(localRotation);
            state.State.LocalPosition = state.InitialLocalPosition + localPosition;
        }
        public static HumanBoneKey Lerp(HumanBoneKey a, HumanBoneKey b, float t)
        {
            return new HumanBoneKey(Quaternion.Lerp(a.localRotation, b.localRotation, t), Vector3.Lerp(a.localPosition, b.localPosition, t));
        }
    }
    protected abstract IEnumerable<HumanBoneAnimation> EnumerataBoneAnimations();

    protected override void Animate()
    {
        foreach (var animation in EnumerataBoneAnimations())
        {
            animation.Apply(LocalTime);
        }
    }
}
