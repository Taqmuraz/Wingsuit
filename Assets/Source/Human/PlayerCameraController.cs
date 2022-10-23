using UnityEngine;

public sealed class PlayerCameraController : ICameraController
{
    IHumanController player;
    Vector3 euler;
    float fov = 60f;
    Vector3 offset = new Vector3(0f, 0f, -1f);
    int humanMask;
    ITransformState target;

    public PlayerCameraController(IHumanController player)
    {
        this.player = player;
        target = player.GetBone("Target");
        humanMask = ~LayerMask.GetMask(HumanController.HumanLayerName, HumanController.HumanElementLayerName);
    }

    public void UpdateCamera(Camera camera, IPlayerCameraMode mode)
    {
        var transform = camera.transform;

        mode.UpdateRotation(ref euler);

        float t = Time.deltaTime * 5f;
        fov = Mathf.Lerp(fov, mode.Fov, t);
        offset = Vector3.Lerp(offset, mode.Offset, t);

        camera.fieldOfView = fov;
        transform.eulerAngles = euler;

        Vector3 dir = transform.TransformVector(offset);
        float distance = dir.magnitude;
        Vector3 target = this.target.Position;
        if (Physics.Raycast(target, dir, out RaycastHit hit, distance, humanMask))
        {
            distance = hit.distance;
        }
        transform.position = target + dir.normalized * distance;
    }
}
