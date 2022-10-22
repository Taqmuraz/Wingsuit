using UnityEngine;

public sealed class TimurZarubinAirForceEngine : IAirForceEngine
{
    public void CalculateAirForce(IWingControl wing, IMoveSystem moveSystem, ITransformState transformState)
    {
        Vector3 globalNormal = wing.WingNormal;
        Vector3 globalPoint = transformState.LocalToWorld.MultiplyPoint3x4(wing.WingPivot);

        float airDensity = 1f;
        float c = 1f;

        Vector3 velocity = moveSystem.GetVelocityAtPoint(globalPoint);

        Vector3 resistanceNormal = globalNormal * -Mathf.Sign(Vector3.Dot(velocity, globalNormal));

        float areaProjection = -Vector3.Dot(resistanceNormal, velocity.normalized) * wing.WingArea;
        float velocityMagnitude = velocity.magnitude;
        float m = 80f;

        float AccelerationPartFunction(float arg)
        {
            return (-c * airDensity * areaProjection * (arg * arg)) / (2 * m);
        }
        float tau = Time.fixedDeltaTime;

        float k1 = AccelerationPartFunction(velocityMagnitude);
        float k2 = AccelerationPartFunction(velocityMagnitude + k1 * tau / 2);
        float k3 = AccelerationPartFunction(velocityMagnitude + k2 * tau / 2);
        float k4 = AccelerationPartFunction(velocityMagnitude + k3 * tau);
        Vector3 resistanceForce = -resistanceNormal * (k1 + 2 * k2 + 2 * k3 + k4) * tau / 6;

        moveSystem.AddVelocityAtPoint(resistanceForce, globalPoint);
    }
}
