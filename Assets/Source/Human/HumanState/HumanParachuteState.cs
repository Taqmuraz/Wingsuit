using System.Collections.Generic;
using UnityEngine;

public sealed partial class HumanParachuteState : HumanAirState
{
    sealed class ParachuteWing : IWingControl
    {
        HumanParachuteState state;

        public ParachuteWing(HumanParachuteState state, Vector3 pivot)
        {
            this.state = state;
            WingPivot = pivot;
        }

        public float WingPotential { get; set; }
        public Vector3 WingNormal => WingPivot.normalized;
        public Vector3 WingPivot { get; }
        float IWingControl.WingArea => WingPotential * 0.25f;
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
        parachuteWingForward = new ParachuteWing(this, new Vector3(0f, 3f, 1f));
        parachuteWingBack = new ParachuteWing(this, new Vector3(0f, 3f, -1f));
        parachuteWingLeft = new ParachuteWing(this, new Vector3(-1f, 3f, 0f));
        parachuteWingRight = new ParachuteWing(this, new Vector3(1f, 3f, 0f));

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
