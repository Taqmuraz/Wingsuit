using UnityEngine;

public class AirplaneWing : MonoBehaviour, IWingControl
{
    [SerializeField] Vector3 normalAxis = Vector3.forward;
    [SerializeField] Vector3 pivotOffset;
    [SerializeField] float wingArea = 1f;

    Airplane airplane;

    public void Initialize(Airplane airplane)
    {
        this.airplane = airplane;
    }

    public float WingArea => wingArea;

    public bool GetResistanceNormal(Vector3 velocity, out Vector3 normal)
    {
        var globalNormal = GlobalNormal;
        normal = globalNormal * -Mathf.Sign(Vector3.Dot(velocity, globalNormal));
        return true;
    }

    Vector3 GlobalNormal => transform.TransformDirection(normalAxis);
    public Vector3 WingPivot => airplane.transform.InverseTransformPoint(transform.TransformPoint(pivotOffset));

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 pivot = transform.TransformPoint(pivotOffset);
        Gizmos.DrawRay(pivot, GlobalNormal);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pivot, 0.05f);
    }
}
