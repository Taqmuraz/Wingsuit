using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class RigidbodyMoveSystem : EventsHandler, IMoveSystem
{
    Rigidbody rigidbody;
    CapsuleCollider collider;
    int humanMask;

    public RigidbodyMoveSystem(Rigidbody rigidbody)
    {
        this.rigidbody = rigidbody;
        EnableFreeRotation = false;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rigidbody.mass = 80f;
        collider = rigidbody.GetComponent<CapsuleCollider>();
        humanMask = ~LayerMask.GetMask(HumanController.HumanLayerName, HumanController.HumanElementLayerName);
    }

    public Vector3 Velocity
    {
        get => rigidbody.velocity;
        set => rigidbody.velocity = value;
    }
    public Vector3 GroundNormal { get; private set; }
    public Vector3 Center => rigidbody.transform.TransformPoint(rigidbody.centerOfMass);

    public void ApplyForce(Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.Acceleration);
    }

    [BehaviourEvent]
    void FixedUpdate()
    {
        IsOnGround = Physics.CheckSphere(rigidbody.transform.position + new Vector3(0f, Size.x * 0.25f, 0f), Size.x * 0.3f, humanMask);
        if (IsOnGround && Physics.Raycast(rigidbody.transform.position + new Vector3(0f, 0.1f, 0f), Vector3.down, out RaycastHit hit, 0.5f, humanMask))
        {
            GroundNormal = hit.normal;
        }
        else GroundNormal = Vector3.up;
    }
    [BehaviourEvent]
    void Update()
    {
    }

    public bool IsOnGround { get; private set; } = true;

    public void Move(Vector3 velocity)
    {
        rigidbody.velocity = new Vector3(velocity.x, rigidbody.velocity.y, velocity.z);
    }

    Vector3 size;
    public Vector3 Size
    {
        get => size;
        set
        {
            size = value;
            collider.radius = value.x * 0.5f;
            collider.height = value.y;
            collider.center = new Vector3(0f, value.y * 0.5f, 0f);
        }
    }

    public bool EnableCollisions
    {
        get => collider.enabled;
        set
        {
            collider.enabled = value;
        }
    }
    public bool EnablePhysics
    {
        get => rigidbody.isKinematic;
        set
        {
            rigidbody.isKinematic = !value;
        }
    }
    bool enableFreeRotation;
    public bool EnableFreeRotation
    {
        get => enableFreeRotation;
        set
        {
            enableFreeRotation = value;
            rigidbody.constraints = value ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeRotation;
        }
    }

    public Vector3 GetVelocityAtPoint(Vector3 point)
    {
        return rigidbody.GetPointVelocity(point);
    }

    public void AddForceAtPoint(Vector3 force, Vector3 point)
    {
        rigidbody.AddForceAtPosition(force, point, ForceMode.Force);
    }
}
