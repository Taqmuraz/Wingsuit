using UnityEngine;

public interface IHumanControlProvider : IControlProvider
{
    Vector3 InputMovement { get; }
    Vector3 InputMovementLocal { get; }
}
