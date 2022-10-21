using UnityEngine;

public sealed class RagdollBoxElementTemplate : RagdollElementTemplate
{
    [SerializeField] Vector3 center;
    [SerializeField] Vector3 size = Vector3.one;

    protected override IRagdollElement CreateElement(JointInfo jointInfo)
    {
        return new RagdollBoxElement(size, center, transform, jointInfo);
    }
    protected override void DrawGizmos()
    {
        Gizmos.DrawWireCube(center, size);
    }
}
