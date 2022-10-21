using UnityEngine;

public interface ICameraController
{
    void UpdateCamera(Camera camera, IPlayerCameraMode mode);
}
