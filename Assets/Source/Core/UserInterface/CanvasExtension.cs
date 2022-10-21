namespace Core.UserInterface.UnityUI
{
    using Core.UserInterface.Basement;

    public static class CanvasExtension
    {
        public static ElementTypeCode GetElementTypeCode<TElement>(this IDrawOptions<TElement> options) where TElement : class, IElement
        {
            return ElementTypeCode.GetElementTypeCode<TElement>();
        }
    }
}