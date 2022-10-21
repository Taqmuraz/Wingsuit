using UnityEngine;

public interface ITransformState
{
    Vector3 Position { get; set; }
    Quaternion Rotation { get; set; }
    Matrix4x4 LocalToWorld { get; }
    Matrix4x4 WorldToLocal { get; }
    Vector3 Forward { get; }
    Vector3 Up { get; }
    Vector3 Right { get; }
}
