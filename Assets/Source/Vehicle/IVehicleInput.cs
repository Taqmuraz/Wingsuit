using UnityEngine;

public interface IVehicleInput
{
    Vector4 ControlInput { get; }
    float AccelerationInput { get; }
}
