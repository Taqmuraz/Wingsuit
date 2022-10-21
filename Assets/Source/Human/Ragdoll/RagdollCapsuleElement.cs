using UnityEngine;

public sealed class RagdollCapsuleElement : RagdollElement<CapsuleCollider>
{
    float radius;
    float height;
    Vector3 center;

    public RagdollCapsuleElement(float radius, float height, Vector3 center, Transform transform, IJointInfo jointInfo) : base(transform, jointInfo)
    {
        this.radius = radius;
        this.height = height;
        this.center = center;
    }

    protected override void SetupCollider(CapsuleCollider collider)
    {
        collider.radius = radius;
        collider.height = height;
        collider.center = center;
    }

    protected override float CalculateMass()
    {
        return Mathf.PI * radius * radius * height;
    }
}