public interface IHumanController : ICollisionSystem, IContainer
{
    IHumanControlProvider ControlProvider { get; }
    IMoveSystem MoveSystem { get; }
    IRagdollSystem RagdollSystem { get; }
    ITransformState TransformState { get; }
    IHierarchyState HierarchyState { get; }
    ITransformState GetBone(string name);
    TElement GetEquipmentElement<TElement>() where TElement : IHumanEquipmentElement;
}
