using UnityEngine;

public interface IMoveSystem : IEventsHandler, IPhysicsBody
{
    Vector3 GroundNormal { get; }
    bool EnableCollisions { get; set; }
    bool EnablePhysics { get; set; }
    bool EnableFreeRotation { get; set; }

    void Move(Vector3 velocity);
    Vector3 Center { get; }
    bool IsOnGround { get; }
    Vector3 Size { get; set; }
}
