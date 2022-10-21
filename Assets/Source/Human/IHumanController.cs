public interface IHumanController
{
    IHumanControlProvider ControlProvider { get; }
    IMoveSystem MoveSystem { get; }
    IRagdollSystem RagdollSystem { get; }
    ITransformState TransformState { get; }
    ITransformState GetBone(string name);
    T GetVariable<T>(string name);
    void SetVariable(string name, object value);
}
