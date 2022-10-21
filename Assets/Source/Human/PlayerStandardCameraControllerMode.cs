using UnityEngine;

public sealed class PlayerStandardCameraControllerMode : IPlayerCameraMode
{
    public Vector3 Offset { get; } = new Vector3(0f, 0f, -4f);
    public float Fov => 60f;
}
