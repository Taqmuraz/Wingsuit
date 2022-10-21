public interface IRagdollSystem
{
    IRagdollState CaptureState();
    void RestoreState(IRagdollState state);
    void SetEnabled(bool enabled);
    void SetElementEnabled(IRagdollElement element, bool enabled);
}
