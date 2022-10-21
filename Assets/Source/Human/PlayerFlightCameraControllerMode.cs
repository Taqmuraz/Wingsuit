using UnityEngine;

public sealed class PlayerFlightCameraControllerMode : IPlayerCameraMode
{
    public Vector3 Offset => new Vector3(0f, 0.75f, -3f);
    public float Fov => 90f;
}
