using UnityEngine;

public interface IMoveSystem : IEventsHandler
{
    Vector3 Velocity { get; set; }
    Vector3 GroundNormal { get; }
    bool EnableCollisions { get; set; }
    bool EnablePhysics { get; set; }
    bool EnableFreeRotation { get; set; }

    void Move(Vector3 velocity);
    Vector3 Center { get; }
    bool IsOnGround { get; }
    void ApplyForce(Vector3 force);
    Vector3 Size { get; set; }

    Vector3 GetVelocityAtPoint(Vector3 point);
    void AddForceAtPoint(Vector3 force, Vector3 point);
}
