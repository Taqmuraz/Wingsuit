﻿using System.Collections.Generic;
using UnityEngine;

public sealed class RagdollSystem : IRagdollSystem
{
    IEnumerable<IRagdollElement> elements;
    Rigidbody rootBody;
    Collider rootCollider;
    Animator rootAnimator;
    IRagdollElement rootElement;

    public RagdollSystem(Animator rootAnimator, Rigidbody rootBody, Collider rootCollider, IRagdollElement rootElement, IRagdollElement[] elements)
    {
        this.elements = elements;
        this.rootCollider = rootCollider;
        this.rootBody = rootBody;
        this.rootElement = rootElement;
        this.rootAnimator = rootAnimator;
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
        if (rootBody != null) rootBody.isKinematic = enabled;
        if (rootCollider != null) rootCollider.enabled = !enabled;

        SetElementEnabled(rootElement, enabled);
    }
    public void SetElementEnabled(IRagdollElement element, bool enabled)
    {
        element.SetEnabled(enabled);
        // animator refresh
        {
            rootAnimator.Rebind();
        }
    }
}