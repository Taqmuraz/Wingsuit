using UnityEngine;

public interface IHumanControlProvider : IControlProvider
{
    Vector3 InputMovement { get; }
    IFlightInput InputFlight { get; }
    Vector3 InputMovementLocal { get; }
}
