using UnityEngine;

public interface IPlayerCameraMode
{
    Vector3 Offset { get; }
    float Fov { get; }
    int IgnoreLayerMask { get; }
    void UpdateRotation(ref Vector3 euler);
}
