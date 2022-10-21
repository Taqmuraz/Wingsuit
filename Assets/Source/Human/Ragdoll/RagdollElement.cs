using System.Collections.Generic;
using UnityEngine;

public abstract class RagdollElement<TCollider> : IRagdollElement where TCollider : Collider
{
    TCollider collider;
    CharacterJoint joint;
    Rigidbody rigidbody;
    Transform transform;
    bool enabled = true;
    IJointInfo jointInfo;
    const float BodyDensity = 1000f;
    public bool IsRoot { get; private set; }
    IEnumerable<IRagdollElement> children;
    string initialName;
    string enabledName;

    protected abstract float CalculateMass();

    Rigidbody FindParentBody()
    {
        var root = transform.root;
        var parent = transform.parent;
        while (parent != root)
        {
            var body = parent.GetComponent<Rigidbody>();
            if (body != null) return body;
            parent = parent.parent;
        }
        return null;
    }

    public RagdollElement(Transform transform, IJointInfo jointInfo)
    {
        this.transform = transform;
        this.jointInfo = jointInfo;
        initialName = transform.name;
        enabledName = $"{initialName}_enabled";
        collider = transform.gameObject.AddComponent<TCollider>();
        rigidbody = transform.gameObject.AddComponent<Rigidbody>();
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void IRagdollElement.Initialize(IRagdollElement[] children)
    {
        this.children = children;

        SetupCollider(collider);

        var parentBody = FindParentBody();
        float mass = rigidbody.mass = CalculateMass() * BodyDensity;

        if (parentBody != null)
        {
            joint = transform.gameObject.AddComponent<CharacterJoint>();
            joint.connectedBody = parentBody;
            joint.connectedAnchor = parentBody.transform.InverseTransformPoint(transform.TransformPoint(jointInfo.ConnectionLocal));
            Matrix4x4 l2w = transform.localToWorldMatrix;
            joint.axis = l2w.MultiplyVector(jointInfo.TwistAxisLocal).normalized;
            joint.swingAxis = l2w.MultiplyVector(jointInfo.SwingAxisLocal).normalized;

            joint.twistLimitSpring = new SoftJointLimitSpring() { spring = mass * jointInfo.TwistSpringForce, damper = 0f };
            joint.swingLimitSpring = new SoftJointLimitSpring() { spring = mass * jointInfo.SwingSpringForce, damper = 0f };
            joint.swing1Limit = new SoftJointLimit() { limit = jointInfo.SwingLimit };
            joint.swing2Limit = new SoftJointLimit() { limit = jointInfo.SwingOrthoLimit };
            joint.lowTwistLimit = new SoftJointLimit() { limit = jointInfo.TwistLowLimit };
            joint.highTwistLimit = new SoftJointLimit() { limit = jointInfo.TwistHighLimit };
        }
        else
        {
            IsRoot = true;
        }
    }
    void IRagdollElement.SetEnabled(bool enabled)
    {
        rigidbody.isKinematic = !enabled;
        this.enabled = enabled;
        transform.name = enabled ? enabledName : initialName;

        foreach (var child in children)
        {
            child.SetEnabled(enabled);
        }
    }

    protected abstract void SetupCollider(TCollider collider);

    void IRagdollElement.WriteState(RagdollElementState state)
    {
        transform.localPosition = state.LocalPosition;
        transform.localRotation = state.LocalRotation;
    }

    RagdollElementState IRagdollElement.ReadState()
    {
        return new RagdollElementState(transform.localPosition, transform.localRotation);
    }
}
