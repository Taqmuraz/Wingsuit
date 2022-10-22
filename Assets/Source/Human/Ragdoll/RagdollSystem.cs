using System.Collections.Generic;
using UnityEngine;

public sealed class RagdollSystem : IRagdollSystem
{
    IEnumerable<IRagdollElement> elements;
    IRagdollElement rootElement;

    public RagdollSystem(IRagdollElement rootElement, IRagdollElement[] elements)
    {
        this.elements = elements;
        this.rootElement = rootElement;
    }

    public IRagdollState CaptureState()
    {
        return new RagdollState(elements);
    }

    public void RestoreState(IRagdollState state)
    {
        foreach (var element in elements) state.ApplyStateToElement(element);
    }

    public void SetEnabled(bool enabled)
    {
        SetElementEnabled(rootElement, enabled);
    }
    public void SetElementEnabled(IRagdollElement element, bool enabled)
    {
        element.SetEnabled(enabled);
    }
}