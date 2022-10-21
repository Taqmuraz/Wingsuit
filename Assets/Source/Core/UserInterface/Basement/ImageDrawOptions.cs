using UnityEngine;

namespace Core.UserInterface.Basement
{
    public sealed class ImageDrawOptions : CommonDrawOptions<IImage>, IImage
    {
        public Texture2D Texture { get; set; }

        public ImageDrawOptions()
        {
        }

        public ImageDrawOptions(Texture2D texture, Rect rect, Color color) : base (rect, color)
        {
            Texture = texture;
        }

        public override void SetupElement(IImage element)
        {
            base.SetupElement(element);
            element.SetTexture(Texture);
        }

        public override IDrawOptions<IImage> CopyFrom(IDrawOptions<IImage> from)
        {
            from.SetupElement(this);
            return this;
        }

        void IImage.SetTexture(Texture2D texture)
        {
            Texture = texture;
        }
    }
}