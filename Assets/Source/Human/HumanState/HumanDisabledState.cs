public sealed class HumanDisabledState : HumanState
{
    sealed class EmptyAnimation : IHumanAnimation
    {
        public void CallEvent(string name)
        {
        }
    }

    protected override IHumanAnimation CreateAnimation()
    {
        return new EmptyAnimation();
    }
    IRagdollState enterState;

    [BehaviourEvent]
    void OnEnter()
    {
        Human.MoveSystem.EnableCollisions = false;
        Human.MoveSystem.EnablePhysics = false;

        enterState = Human.RagdollSystem.CaptureState();
        Human.RagdollSystem.SetEnabled(true);
    }
    [BehaviourEvent]
    void OnExit()
    {
        Human.RagdollSystem.SetEnabled(false);
        Human.RagdollSystem.RestoreState(enterState);

        Human.MoveSystem.EnableCollisions = true;
        Human.MoveSystem.EnablePhysics = true;
    }
}
