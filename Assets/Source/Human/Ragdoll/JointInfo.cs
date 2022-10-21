using UnityEngine;

[System.Serializable]
public sealed class JointInfo : IJointInfo
{
    [SerializeField] Vector3 connectionLocal = Vector3.zero;
    [SerializeField] Vector3 swingAxisLocal = Vector3.up;
    [SerializeField] Vector3 twistAxisLoca = Vector3.right;
    [SerializeField] float twistSpringForce = 20f;
    [SerializeField] float swingSpringForce = 5f;
    [SerializeField] float twistLowLimit = -45f;
    [SerializeField] float twistHightLimit = -5f;
    [SerializeField] float swingLimit = 5f;
    [SerializeField] float swingOrthoLimit = 0f;

    public Vector3 ConnectionLocal => connectionLocal;
    public Vector3 SwingAxisLocal => swingAxisLocal;
    public Vector3 TwistAxisLocal => twistAxisLoca;

    public float TwistSpringForce => twistSpringForce;
    public float SwingSpringForce => swingSpringForce;
    public float TwistHighLimit => twistHightLimit;
    public float TwistLowLimit => twistLowLimit;
    public float SwingLimit => swingLimit;
    public float SwingOrthoLimit => swingOrthoLimit;
}
