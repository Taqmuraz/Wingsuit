using UnityEngine;

public sealed class RagdollSphereElementTemplate : RagdollElementTemplate
{
    [SerializeField] Vector3 center;
    [SerializeField] float radius = 0.5f;
    protected override IRagdollElement CreateElement(JointInfo jointInfo)
    {
        return new RagdollSphereElement(radius, center, transform, jointInfo);
    }
    protected override void DrawGizmos()
    {
        Gizmos.DrawWireSphere(center, radius);
    }
}
