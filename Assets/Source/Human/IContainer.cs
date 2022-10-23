public interface IContainer
{
    T GetVariable<T>(string name);
    void SetVariable(string name, object value);
}
