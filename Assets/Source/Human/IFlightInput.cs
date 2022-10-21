public interface IFlightInput
{
    float CommonWingsOpenness { get; }
    float ForwardWingsOpenness { get; }
    float BackWingsOpenness { get; }
    float LeftWingOpenness { get; }
    float RightWingOpenness { get; }
    float ForwardWingRotationNormalized { get; }
    float BackWingRotationNormalized { get; }
}
