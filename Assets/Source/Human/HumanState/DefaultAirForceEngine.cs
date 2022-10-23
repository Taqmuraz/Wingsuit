using UnityEngine;

public sealed class DefaultAirForceEngine : IAirForceEngine
{
    public void CalculateAirForce(IWingControl wing, IPhysicsBody physicsBody, ITransformState transformState)
    {
        Vector3 globalPoint = transformState.LocalToWorld.MultiplyPoint3x4(wing.WingPivot);

        Vector3 velocity = physicsBody.GetVelocityAtPoint(globalPoint);

        if (!wing.GetResistanceNormal(velocity, out Vector3 resistanceNormal)) return;

        float areaProjection = -Vector3.Dot(resistanceNormal, velocity.normalized);
        float velocityMagnitude2 = velocity.magnitude * velocity.magnitude;

        Vector3 resistance = resistanceNormal * areaProjection * velocityMagnitude2 * wing.WingArea;
        physicsBody.AddForceAtPoint(resistance, globalPoint);
    }
}
