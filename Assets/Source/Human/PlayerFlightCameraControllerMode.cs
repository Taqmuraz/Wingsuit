using UnityEngine;

public sealed class PlayerFlightCameraControllerMode : IPlayerCameraMode
{
    public Vector3 Offset => new Vector3(1f, 0.75f, -2f);
    public float Fov => 45f;
}
