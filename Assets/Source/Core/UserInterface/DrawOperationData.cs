namespace Core.UserInterface
{
    using Core.UserInterface.Basement;
    using Core.Operations;
    using Core.Abstractions;

    public class DrawOperationData : IOperationData
    {
        int typeCode;
        public IElement Element { get; private set; }

        public DrawOperationData(int typeCode, IElement element)
        {
            this.typeCode = typeCode;
            Element = element;
        }

        public int GetDataTypeCode()
        {
            return typeCode;
        }

        public void Release()
        {
            if (Element is IReleasable) ((IReleasable)Element).Release();
        }
    }
}
