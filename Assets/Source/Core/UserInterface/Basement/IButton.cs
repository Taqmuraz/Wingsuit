namespace Core.UserInterface.Basement
{
    using Core.Abstractions;

    public interface IButton : IElement
    {
        void SetupText(IDrawOptions<IText> textOptions);
        void SetupImage(IDrawOptions<IImage> imageOptions);
    }
}