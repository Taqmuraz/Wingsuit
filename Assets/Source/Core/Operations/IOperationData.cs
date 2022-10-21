namespace Core.Operations
{
    using Core.Abstractions;

    public interface IOperationData : IReleasable
    {
        int GetDataTypeCode();
    }
}
