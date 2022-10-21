using UnityEngine;

public struct RagdollElementState
{
    public RagdollElementState(Vector3 localPosition, Quaternion localRotation)
    {
        LocalPosition = localPosition;
        LocalRotation = localRotation;
    }
    public Vector3 LocalPosition { get; }
    public Quaternion LocalRotation { get; }

    public static RagdollElementState Lerp(RagdollElementState a, RagdollElementState b, float blend)
    {
        return new RagdollElementState
            (Vector3.Lerp(a.LocalPosition, b.LocalPosition, blend),
            Quaternion.Lerp(a.LocalRotation, b.LocalRotation, blend));
    }
}
