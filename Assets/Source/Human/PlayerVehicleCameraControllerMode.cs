using UnityEngine;

public sealed class PlayerVehicleCameraControllerMode : IPlayerCameraMode
{
    IHumanController human;
    int ignoreMask;

    public PlayerVehicleCameraControllerMode(IHumanController human)
    {
        this.human = human;
        ignoreMask = ~LayerMask.GetMask(HumanController.HumanLayerName, HumanController.HumanElementLayerName, HumanController.VehicleLayerName);
    }

    public Vector3 Offset => new Vector3(0f, 1.5f, -6f);
    public float Fov => 60f;

    public void UpdateRotation(ref Vector3 euler)
    {
        Quaternion next = Quaternion.LookRotation(human.TransformState.Forward, Vector3.up);
        euler = Quaternion.Lerp(Quaternion.Euler(euler), next, Time.deltaTime * 5f).eulerAngles;
    }

    public int IgnoreLayerMask => ignoreMask;
}
