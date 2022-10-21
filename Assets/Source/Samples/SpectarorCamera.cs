using UnityEngine;

public sealed class SpectarorCamera : MonoBehaviour
{
    [SerializeField] bool zooming = true;
    [SerializeField] float zoom = 1f;
    [SerializeField] Transform target;
    [SerializeField] new Camera camera;

    private void OnPreRender()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = target.position;
        if (zooming)
        {
            float dist = (pos - targetPos).magnitude;
            float g = Mathf.Sqrt(dist * dist + zoom * zoom);

            float anglePerMeter = Mathf.Asin(zoom / g) * Mathf.Rad2Deg;
            camera.fieldOfView = anglePerMeter;
        }
        else
        {
            camera.fieldOfView = 30f;
        }

        camera.transform.rotation = Quaternion.LookRotation(targetPos - pos);
    }
}