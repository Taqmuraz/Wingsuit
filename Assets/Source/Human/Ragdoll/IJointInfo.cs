using UnityEngine;

public interface IJointInfo
{
    Vector3 ConnectionLocal { get; }
    Vector3 SwingAxisLocal { get; }
    Vector3 TwistAxisLocal { get; }
    float TwistSpringForce { get; }
    float SwingSpringForce { get; }
    float TwistHighLimit { get; }
    float TwistLowLimit { get; }
    float SwingLimit { get; }
    float SwingOrthoLimit { get; }
}
