using UnityEngine;

public sealed class RagdollSphereElement : RagdollElement<SphereCollider>
{
    float radius;
    Vector3 center;

    public RagdollSphereElement(float radius, Vector3 center, Transform transform, IJointInfo jointInfo) : base(transform, jointInfo)
    {
        this.radius = radius;
        this.center = center;
    }

    protected override void SetupCollider(SphereCollider collider)
    {
        collider.radius = radius;
        collider.center = center;
    }
    protected override float CalculateMass()
    {
        return (4f / 3f) * Mathf.PI * (radius * radius * radius);
    }
}
