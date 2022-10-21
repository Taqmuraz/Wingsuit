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
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
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

    public bool IsOnGround { get; private set; }

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

    bool enabled = true;
    public bool Enabled
    {
        get => enabled;
        set
        {
            rigidbody.isKinematic = !value;
            collider.enabled = value;
        }
    }
}
