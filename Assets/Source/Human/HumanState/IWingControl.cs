using UnityEngine;

public interface IWingControl
{
    float WingArea { get; }
    Vector3 WingNormal { get; }
    Vector3 WingPivot { get; }
}
