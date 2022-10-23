using System.Collections.Generic;
using UnityEngine;

public sealed partial class HumanParachuteState : HumanAirState
{
    sealed class ParachuteWing : IWingControl
    {
        HumanParachuteState state;
        Vector3 localNormal;

        public ParachuteWing(HumanParachuteState state, Vector3 pivot, Vector3 localNormal)
        {
            this.state = state;
            WingPivot = pivot;
            this.localNormal = localNormal;
        }

        public float WingPotential { get; set; }
        public Vector3 WingPivot { get; }
        float IWingControl.WingArea => WingPotential * 5f;

        bool IWingControl.GetResistanceNormal(Vector3 velocity, out Vector3 normal)
        {
            normal = state.Human.TransformState.Rotation * localNormal;
            return Vector3.Dot(velocity, normal) < 0f;
        }
    }

    protected override void OnLanded(Vector3 point, Vector3 normal, Vector3 impulse)
    {
        MoveToState("ParachuteToGround");
    }

    HumanParachuteEquipmentElement parachute;
    ParachuteWing parachuteWingForward;
    ParachuteWing parachuteWingBack;
    ParachuteWing parachuteWingLeft;
    ParachuteWing parachuteWingRight;
    float enterTime;
    const float parachuteOpenTimeLength = 1f;

    [BehaviourEvent]
    void Initialize()
    {
        parachuteWingForward = new ParachuteWing(this, new Vector3(0f, 3f, 1f), new Vector3(0f, 1f, 1f).normalized);
        parachuteWingBack = new ParachuteWing(this, new Vector3(0f, 3f, -1f), new Vector3(0f, 1f, -1f).normalized);
        parachuteWingLeft = new ParachuteWing(this, new Vector3(-1f, 3f, 0f), new Vector3(-1f, 1f, 0f).normalized);
        parachuteWingRight = new ParachuteWing(this, new Vector3(1f, 3f, 0f), new Vector3(1f, 1f, 0f).normalized);

        parachute = Human.GetEquipmentElement<HumanParachuteEquipmentElement>();
    }

    [BehaviourEvent]
    void Update()
    {
        float commonArea = Mathf.Clamp01((Time.time - enterTime) / parachuteOpenTimeLength);

        parachuteWingBack.WingPotential = commonArea;
        parachuteWingForward.WingPotential = commonArea;
        parachuteWingLeft.WingPotential = commonArea;
        parachuteWingRight.WingPotential = commonArea;
    }

    [BehaviourEvent]
    void OnEnter()
    {
        parachute.Enable();
        enterTime = Time.time;
    }
    [BehaviourEvent]
    void OnExit()
    {
        parachute.Disable();
    }

    protected override IEnumerable<IWingControl> EnumerateControls()
    {
        yield return parachuteWingForward;
        yield return parachuteWingBack;
        yield return parachuteWingLeft;
        yield return parachuteWingRight;
    }

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanParachuteAnimation(Human, this);
    }
}
