public interface IHumanController : ICollisionSystem
{
    IHumanControlProvider ControlProvider { get; }
    IMoveSystem MoveSystem { get; }
    IRagdollSystem RagdollSystem { get; }
    ITransformState TransformState { get; }
    ITransformState GetBone(string name);
    TElement GetEquipmentElement<TElement>() where TElement : IHumanEquipmentElement;
    T GetVariable<T>(string name);
    void SetVariable(string name, object value);
}
