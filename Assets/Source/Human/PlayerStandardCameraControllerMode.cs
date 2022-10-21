using UnityEngine;

public sealed class PlayerStandardCameraControllerMode : IPlayerCameraMode
{
    public Vector3 Offset { get; } = new Vector3(0f, 0f, -4f);
    public float Fov => 60f;

    public void UpdateRotation(ref Vector3 euler)
    {
        Vector3 input = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);
        euler += input;
        euler.x = Mathf.Clamp(euler.x, -80f, 80f);
        euler.z = 0f;
    }
}
