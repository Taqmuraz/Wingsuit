using UnityEngine;

public sealed class StandardTransformState : ITransformState, IHierarchyState
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
    public Vector3 LocalPosition
    {
        get => transform.localPosition;
        set => transform.localPosition = value;
    }
    public Vector3 LocalScale
    {
        get => transform.localScale;
        set => transform.localScale = value;
    }
    public Quaternion LocalRotation
    {
        get => transform.localRotation;
        set => transform.localRotation = value;
    }
    public Matrix4x4 LocalToWorld => transform.localToWorldMatrix;
    public Matrix4x4 WorldToLocal => transform.worldToLocalMatrix;
    public Vector3 Forward => transform.forward;
    public Vector3 Up => transform.up;
    public Vector3 Right => transform.right;

    public void ApplyParent(Transform parent, Vector3 localPosition, Quaternion localRotation)
    {
        transform.parent = parent;
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;
    }
}