using UnityEngine;

public sealed class PlayerFlightCameraControllerMode : IPlayerCameraMode
{
    IHumanController human;

    public PlayerFlightCameraControllerMode(IHumanController human)
    {
        this.human = human;
    }

    public Vector3 Offset => new Vector3(0f, 0.75f, -3f);
    public float Fov => 60f;

    public void UpdateRotation(ref Vector3 euler)
    {
        Quaternion next = Quaternion.LookRotation(human.MoveSystem.Velocity.normalized, Vector3.up);
        euler = Quaternion.Lerp(Quaternion.Euler(euler), next, Time.deltaTime * 5f).eulerAngles;
    }
}
