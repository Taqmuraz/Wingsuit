using UnityEngine;

public interface IRagdollElement
{
    void Initialize(IHumanController human, IRagdollElement[] children);
    void WriteState(RagdollElementState state);
    RagdollElementState ReadState();

    void SetEnabled(bool enabled);
    bool IsRoot { get; }
}
