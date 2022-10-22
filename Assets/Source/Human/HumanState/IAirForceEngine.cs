public interface IAirForceEngine
{
    void CalculateAirForce(IWingControl wing, IMoveSystem moveSystem, ITransformState transformState);
}
