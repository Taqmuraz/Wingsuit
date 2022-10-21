using UnityEngine;

public sealed class StandardTransformState : ITransformState
{
    Transform transform;

    public StandardTransformState(Transform transform)
    {
        this.transform = transform;
    }

    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
    public Quaternion Rotation
    {
        get => transform.rotation;
        set => transform.rotation = value;
    }
    public Matrix4x4 LocalToWorld => transform.localToWorldMatrix;
    public Matrix4x4 WorldToLocal => transform.worldToLocalMatrix;
    public Vector3 Forward => transform.forward;
    public Vector3 Up => transform.up;
    public Vector3 Right => transform.right;
}