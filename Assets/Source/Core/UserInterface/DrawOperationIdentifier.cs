namespace Core.UserInterface
{
    using Core.Operations;

    public class DrawOperationIdentifier : IOperation<DrawOperationData>
    {
        public bool MarkToDelete { get; set; }
    }
}
