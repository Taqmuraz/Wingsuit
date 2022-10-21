using UnityEngine;

public sealed class RagdollBoxElement : RagdollElement<BoxCollider>
{
    Vector3 size;
    Vector3 center;

    public RagdollBoxElement(Vector3 size, Vector3 center, Transform transform, IJointInfo jointInfo) : base(transform, jointInfo)
    {
        this.size = size;
        this.center = center;
    }

    protected override void SetupCollider(BoxCollider collider)
    {
        collider.center = center;
        collider.size = size;
    }
    protected override float CalculateMass()
    {
        return size.x * size.y * size.z;
    }
}
