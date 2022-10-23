using UnityEngine;

public sealed class TestHumanController : HumanController, IHumanControlProvider
{
    public override IHumanControlProvider ControlProvider => this;

    protected override IMoveSystem CreateMoveSystem()
    {
        return new RigidbodyMoveSystem(gameObject);
    }

    public IControlAction GetAction()
    {
        return EmptyAction.Instance;
    }

    public Vector3 InputMovement { get; }
    public Vector3 SourceView => Vector3.forward;
    public Vector3 DesiredView => Vector3.forward;
    public Vector3 InputMovementLocal { get; }
    public IFlightInput InputFlight { get; }
    public IVehicleInput VehicleInput { get; }
}
