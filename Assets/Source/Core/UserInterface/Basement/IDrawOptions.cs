namespace Core.UserInterface.Basement
{
    public interface IDrawOptions<TElement> where TElement : class, IElement
    {
        void SetupElement(TElement element);
        IDrawOptions<TElement> CopyFrom(IDrawOptions<TElement> from);
    }
}