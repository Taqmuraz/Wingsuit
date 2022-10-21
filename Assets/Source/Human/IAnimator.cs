public interface IAnimator
{
    void PlayAnimation(string name);
    bool IsPlayingAnimation(string name);
    void SetFloat(string name, float value);
}
