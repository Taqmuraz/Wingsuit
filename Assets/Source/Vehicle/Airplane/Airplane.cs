using System;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : Usable, IVehicle
{
    sealed class UnuseAirplaneAction : IControlAction
    {
        public void VisitAcceptor(IControlAcceptor acceptor)
        {
            if (acceptor.State == "Vehicle")
                acceptor.MoveToState("Flight");
            else acceptor.MoveToState("Vehicle");
        }
    }

    [SerializeField] Transform inState;
    [SerializeField] Transform outState;
    AirplaneWing[] wings;
    [SerializeField] Transform cross;
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] AirplaneStabilizer leftWingStabilizer;
    [SerializeField] AirplaneStabilizer rightWingStabilizer;
    [SerializeField] AirplaneStabilizer backLeftWingStabilizer;
    [SerializeField] AirplaneStabilizer backRightWingStabilizer;
    [SerializeField] AirplaneStabilizer backUpWingStabilizer;

    IAirForceEngine airForceEngine = new DefaultAirForceEngine();
    ITransformState transformState;
    float acceleration;
    [SerializeField] float accelerationVelocity = 0.1f;
    [SerializeField] Vector3 crossAxis = Vector3.forward;

    void Start()
    {
        transformState = new StandardTransformState(transform);
        wings = GetComponentsInChildren<AirplaneWing>();
        foreach (var wing in wings) wing.Initialize(this);
    }

    public void InputHierarchy(IHierarchyState hierarchy)
    {
        hierarchy.ApplyParent(inState, Vector3.zero, Quaternion.identity);
    }

    public void OutputHierarchy(IHierarchyState hierarchy)
    {
        hierarchy.ApplyParent(null, outState.position, outState.rotation);
    }

    public void Control(IVehicleInput vehicleInput)
    {
        var control = vehicleInput.ControlInput;
        leftWingStabilizer.UpdateTurnAngle(-control.x);
        rightWingStabilizer.UpdateTurnAngle(control.x);

        backLeftWingStabilizer.UpdateTurnAngle(-control.y);
        backRightWingStabilizer.UpdateTurnAngle(-control.y);
        backUpWingStabilizer.UpdateTurnAngle(control.z);

        acceleration = vehicleInput.AccelerationInput;
    }

    void FixedUpdate()
    {
        foreach (var wing in wings)
        {
            airForceEngine.CalculateAirForce(wing, this, transformState);
        }
        rigidbody.AddForceAtPosition(cross.TransformDirection(crossAxis) * acceleration * accelerationVelocity, cross.position, ForceMode.VelocityChange);
        cross.localRotation *= Quaternion.AngleAxis(rigidbody.velocity.magnitude * (1 + acceleration) * Time.deltaTime * 100f, crossAxis);
    }

    Vector3 IPhysicsBody.Velocity => rigidbody.velocity;
    float IPhysicsBody.Mass => rigidbody.mass;

    void IPhysicsBody.AddForceAtPoint(Vector3 force, Vector3 point)
    {
        rigidbody.AddForceAtPosition(force, point, ForceMode.Force);
    }

    void IPhysicsBody.AddVelocityAtPoint(Vector3 velocity, Vector3 point)
    {
        rigidbody.AddForceAtPosition(velocity, point, ForceMode.VelocityChange);
    }

    Vector3 IPhysicsBody.GetVelocityAtPoint(Vector3 point)
    {
        return rigidbody.GetPointVelocity(point);
    }

    protected override Bounds Bounds => new Bounds(transform.TransformPoint(rigidbody.centerOfMass), new Vector3(3f, 3f, 3f));

    public override IControlAction Use(IUser user)
    {
        if (ReferenceEquals(user.GetVariable<IVehicle>("vehicle"), this))
        {
            return new UnuseAirplaneAction();
        }
        else
        {
            user.SetVariable("vehicle", this);
            return new UseVehicleAction();
        }
    }

    public override string Description => "Airplane";

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(rigidbody.centerOfMass), 0.1f);
    }
}