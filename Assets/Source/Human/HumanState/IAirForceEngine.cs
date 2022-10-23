public interface IAirForceEngine
{
    void CalculateAirForce(IWingControl wing, IPhysicsBody physicsBody, ITransformState transformState);
}
