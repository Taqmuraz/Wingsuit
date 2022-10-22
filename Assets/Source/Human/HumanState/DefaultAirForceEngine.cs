using UnityEngine;

public sealed class DefaultAirForceEngine : IAirForceEngine
{
    public void CalculateAirForce(IWingControl wing, IMoveSystem moveSystem, ITransformState transformState)
    {
        Vector3 globalNormal = wing.WingNormal;
        Vector3 globalPoint = transformState.LocalToWorld.MultiplyPoint3x4(wing.WingPivot);

        Vector3 velocity = moveSystem.GetVelocityAtPoint(globalPoint);

        Vector3 resistanceNormal = globalNormal * -Mathf.Sign(Vector3.Dot(velocity, globalNormal));

        float areaProjection = -Vector3.Dot(resistanceNormal, velocity.normalized);
        float velocityMagnitude2 = velocity.magnitude * velocity.magnitude;

        Vector3 resistance = resistanceNormal * areaProjection * velocityMagnitude2 * wing.WingArea;
        moveSystem.AddForceAtPoint(resistance, globalPoint);
    }
}
