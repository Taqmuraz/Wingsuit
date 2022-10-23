using UnityEngine;

public interface IWingControl
{
    float WingArea { get; }
    bool GetResistanceNormal(Vector3 velocity, out Vector3 normal);
    Vector3 WingPivot { get; }
}
