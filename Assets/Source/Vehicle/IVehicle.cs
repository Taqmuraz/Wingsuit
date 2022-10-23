public interface IVehicle : IPhysicsBody
{
    void InputHierarchy(IHierarchyState hierarchy);
    void OutputHierarchy(IHierarchyState hierarchy);
    void Control(IVehicleInput vehicleInput);
}
