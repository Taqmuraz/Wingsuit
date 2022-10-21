namespace Core.Operations
{
    public interface IOperation<TData>
    {
        bool MarkToDelete { get; set; }
    }
}
