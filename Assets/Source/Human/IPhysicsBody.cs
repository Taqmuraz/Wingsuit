using UnityEngine;

public interface IPhysicsBody
{
    Vector3 Velocity { get; }
    float Mass { get; }
    void AddForceAtPoint(Vector3 force, Vector3 point);
    void AddVelocityAtPoint(Vector3 velocity, Vector3 point);
    Vector3 GetVelocityAtPoint(Vector3 point);
}
