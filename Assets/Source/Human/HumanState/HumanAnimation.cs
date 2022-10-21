public abstract class HumanAnimation : EventsHandler, IHumanAnimation
{
    protected IHumanController Human { get; }
    protected ITransformState LeftLeg { get; }
    protected ITransformState RightLeg { get; }
    protected ITransformState LeftArm { get; }
    protected ITransformState RightArm { get; }
    protected ITransformState LeftForeArm { get; }
    protected ITransformState RightForeArm { get; }
    protected ITransformState Spine { get; }
    protected ITransformState LeftKnee { get; }
    protected ITransformState RightKnee { get; }

    public HumanAnimation(IHumanController human)
    {
        this.Human = human;
        LeftLeg = human.GetBone("LeftLeg");
        RightLeg = human.GetBone("RightLeg");
        LeftArm = human.GetBone("LeftArm");
        RightArm = human.GetBone("RightArm");
        LeftForeArm = human.GetBone("LeftForeArm");
        RightForeArm = human.GetBone("RightForeArm");
        Spine = human.GetBone("Spine");
        LeftKnee = human.GetBone("LeftKnee");
        RightKnee = human.GetBone("RightKnee");
    }

    [BehaviourEvent]
    void Update()
    {
        Animate();
    }
    protected abstract void Animate();
}