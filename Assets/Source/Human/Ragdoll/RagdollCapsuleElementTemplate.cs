using UnityEngine;

public sealed class RagdollCapsuleElementTemplate : RagdollElementTemplate
{
    [SerializeField] float radius = 0.5f;
    [SerializeField] float length = 1f;
    [SerializeField] int boneAxis = 1;
    Vector3 Center => new Vector3() { [boneAxis] = length * 0.5f };

    protected override IRagdollElement CreateElement(JointInfo jointInfo)
    {
        return new RagdollCapsuleElement(radius, length, Center, transform, jointInfo);
    }
    protected override void DrawGizmos()
    {
        Gizmos.DrawWireCube(Center, new Vector3(radius * 2f, length, radius * 2f));
    }
}