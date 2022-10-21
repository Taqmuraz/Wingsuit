using UnityEngine;

partial class HumanDefaultState
{
    public sealed class HumanWalkAnimation : HumanAnimation
    {
        HumanDefaultState state;

        public HumanWalkAnimation(IHumanController human, HumanDefaultState state) : base(human)
        {
            this.state = state;
        }

        protected override void Animate()
        {
            float sin = Mathf.Sin(LocalTime * Mathf.PI * 2f) * (Human.MoveSystem.Velocity.WithY(0f).magnitude / state.MoveSpeed);

            LeftLeg.Rotate(Quaternion.AngleAxis(sin * 30f, Vector3.left));
            RightLeg.Rotate(Quaternion.AngleAxis(sin * 30f, Vector3.right));

            LeftArm.Rotate(Quaternion.AngleAxis(sin * 30f, Vector3.right));
            RightArm.Rotate(Quaternion.AngleAxis(sin * 30f, Vector3.left));
        }
    }
}