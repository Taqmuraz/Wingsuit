using UnityEngine;

public interface IFlightInput
{
    float CommonWingsOpenness { get; }
    float ForwardWingsOpenness { get; }
    float BackWingsOpenness { get; }
    float LeftWingOpenness { get; }
    float RightWingOpenness { get; }
    Vector2 LeftWingRotationNormalized { get; }
    Vector2 RightWingRotationNormalized { get; }
    Vector2 BackWingRotationNormalized { get; }
}
