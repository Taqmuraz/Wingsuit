namespace Core.UserInterface.UnityUI
{
    using UnityEngine;
    using Core.UserInterface.Basement;
    using Core.Operations;

    public abstract class OperationsBasedCanvas<TElementConstructorArg> : MonoBehaviour, IUserInterfaceCanvas
    {
        OperationsQueue<DrawOperationIdentifier, DrawOperationData> operationsQueue = new OperationsQueue<DrawOperationIdentifier, DrawOperationData>();

        protected abstract TElement CreateElement<TElement>(TElementConstructorArg arg) where TElement : IElement;

        protected virtual void HandleElement<TElement>(IElement element, IDrawOptions<TElement> options) where TElement : class, IElement
        {
            options.SetupElement((TElement)element);
        }

        protected void DrawElement<TElement>(IDrawOptions<TElement> options, TElementConstructorArg arg) where TElement : class, IElement
        {
            int typeCode = options.GetElementTypeCode();
            operationsQueue.HandleOperationData(typeCode, element => HandleElement(element.Element, options), () => new DrawOperationData(typeCode, CreateElement<TElement>(arg)));
        }

        public abstract void DrawText(IDrawOptions<IText> textOptions);
        public abstract void DrawImage(IDrawOptions<IImage> imageOptions);
        public abstract bool DrawButton(IDrawOptions<IButton> buttonOptions);
        public abstract void DrawLine(IDrawOptions<ILine> lineOptions);
        public abstract void DrawPoint(IDrawOptions<IPoint> pointOptions);
        public abstract float DrawScrollbar(IDrawOptions<IScrollbar> scrollbarOptions);

        public void BeginDraw(DrawOperationIdentifier operation)
        {
            operationsQueue.BeginOperation(operation);
        }

        public void EndDraw()
        {
            operationsQueue.EndOperation();
        }

        public void Clear()
        {
            operationsQueue.RefreshOperations();
        }

        public abstract void Release();
    }
}