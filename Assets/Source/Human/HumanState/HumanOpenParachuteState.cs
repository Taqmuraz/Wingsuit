using UnityEngine;

public sealed class HumanOpenParachuteState : HumanState
{
    public sealed class HumanOpenParachuteAnimation : HumanAnimation
    {
        HumanOpenParachuteState state;

        public HumanOpenParachuteAnimation(IHumanController human, HumanOpenParachuteState state) : base(human)
        {
            this.state = state;
        }

        protected override bool ResetStateOnEnter => false;

        protected override void Animate()
        {
            float t = LocalTime / state.transitionLength;
            Head.Rotate(Quaternion.Euler(Mathf.Lerp(45f, -45f, t), 0f, 0f));

            LeftArm.Rotate(Quaternion.Euler(Mathf.Lerp(90f, -45f, t), 0f, -45f));
            LeftForearm.Rotate(Quaternion.Euler(Mathf.Lerp(135f, 0f, t), 0f, 0f));
        }
    }
    float transitionLength = 0.5f;
    Timer exitTimer;
    [BehaviourEvent]
    void OnEnter()
    {
        exitTimer = new Timer(transitionLength);
    }

    [BehaviourEvent]
    void Update()
    {
        if (exitTimer.IsOver) MoveToState("Parachute");
    }

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanOpenParachuteAnimation(Human, this);
    }
}
