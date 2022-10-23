using UnityEngine;

public sealed class AirplaneStabilizer : AirplaneWing
{
    [SerializeField] Vector3 turnAxis = Vector3.forward;
    [SerializeField] float turnAngle = 15f;
    float currentAngle;
    MemorizedTransformState memorizedState;

    private void Awake()
    {
        memorizedState = new MemorizedTransformState(new StandardTransformState(transform));
    }

    public void UpdateTurnAngle(float turnAngleNormalized)
    {
        currentAngle = Mathf.Lerp(currentAngle, turnAngleNormalized * turnAngle, Time.deltaTime * 5f);
        memorizedState.Rotate(Quaternion.AngleAxis(currentAngle, turnAxis));
    }
}
