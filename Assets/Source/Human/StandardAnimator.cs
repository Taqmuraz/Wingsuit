using UnityEngine;

public sealed class StandardAnimator : IAnimator
{
    Animator animator;
    Transform[] tracedBones;

    public StandardAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void PlayAnimation(string name)
    {
        animator.Play(name);
    }

    public void SetFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }

    public bool IsPlayingAnimation(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}