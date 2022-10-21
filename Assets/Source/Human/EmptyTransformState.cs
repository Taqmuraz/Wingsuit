using UnityEngine;

public sealed class EmptyTransformState : ITransformState
{
    public Vector3 Position
    {
        get => Vector3.zero;
        set { }
    }

    public Quaternion Rotation
    {
        get => Quaternion.identity;
        set { }
    }
    public Matrix4x4 LocalToWorld
    {
        get => Matrix4x4.identity;
        set { }
    }
    public Matrix4x4 WorldToLocal
    {
        get => Matrix4x4.identity;
        set { }
    }
    public Vector3 Forward
    {
        get => Vector3.forward;
        set { }
    }
    public Vector3 Up
    {
        get => Vector3.up;
        set { }
    }
    public Vector3 Right
    {
        get => Vector3.right;
        set { }
    }

    public Vector3 LocalPosition
    {
        get => Vector3.zero;
        set { }
    }
    public Vector3 LocalScale
    {
        get => Vector3.one;
        set { }
    }
    public Quaternion LocalRotation
    {
        get => Quaternion.identity;
        set { }
    }
}