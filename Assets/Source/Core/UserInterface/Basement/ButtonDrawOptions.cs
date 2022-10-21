using UnityEngine;

namespace Core.UserInterface.Basement
{

    public sealed class ButtonDrawOptions : IDrawOptions<IButton>, IButton
    {
        public IDrawOptions<IText> Text { get; set; }
        public IDrawOptions<IImage> Image { get; set; }

        public ButtonDrawOptions()
        {
        }

        public ButtonDrawOptions(IDrawOptions<IText> text, IDrawOptions<IImage> image)
        {
            Text = text;
            Image = image;
        }

        public void SetupElement(IButton element)
        {
            if (Text != null) element.SetupText(Text);
            if (Image != null) element.SetupImage(Image);
        }

        void IButton.SetupText(IDrawOptions<IText> textOptions)
        {
            Text = textOptions;
        }

        void IButton.SetupImage(IDrawOptions<IImage> imageOptions)
        {
            Image = imageOptions;
        }

        public IDrawOptions<IButton> CopyFrom(IDrawOptions<IButton> from)
        {
            from.SetupElement(this);
            return this;
        }
    }
}