using UnityEngine;

public interface IPlayerCameraMode
{
    Vector3 Offset { get; }
    float Fov { get; }
}
