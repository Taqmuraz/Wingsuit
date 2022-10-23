using UnityEngine;

public sealed class HumanVehicleState : HumanState
{
    IVehicle vehicle;

    sealed class HumanVehicleAnimation : HumanAnimation
    {
        public HumanVehicleAnimation(IHumanController human) : base(human)
        {
        }

        protected override void Animate()
        {
            LeftForearm.Rotate(Quaternion.Euler(90f, 0f, 0f));
            RightForearm.Rotate(Quaternion.Euler(90f, 0f, 0f));
            LeftLeg.Rotate(Quaternion.Euler(90f, 0f, 0f));
            RightLeg.Rotate(Quaternion.Euler(90f, 0f, 0f));
        }
    }

    protected override IHumanAnimation CreateAnimation()
    {
        return new HumanVehicleAnimation(Human);
    }

    [BehaviourEvent]
    void OnEnter()
    {
        vehicle = Human.GetVariable<IVehicle>("vehicle");
        vehicle.InputHierarchy(Human.HierarchyState);

        Human.MoveSystem.EnableCollisions = false;
        Human.MoveSystem.EnablePhysics = false;
    }
    [BehaviourEvent]
    void Update()
    {
        vehicle.Control(Human.ControlProvider.VehicleInput);
    }
    [BehaviourEvent]
    void OnExit()
    {
        vehicle.OutputHierarchy(Human.HierarchyState);

        Human.MoveSystem.EnableCollisions = true;
        Human.MoveSystem.EnablePhysics = true;

        Human.MoveSystem.AddVelocityAtPoint(vehicle.Velocity, Human.MoveSystem.Center);
    }
}
