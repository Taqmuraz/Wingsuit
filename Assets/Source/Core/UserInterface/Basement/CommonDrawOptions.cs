using UnityEngine;

namespace Core.UserInterface.Basement
{
    public abstract class CommonDrawOptions<TElement> : IDrawableElement, IDrawOptions<TElement> where TElement : class, IDrawableElement
    {
        public CommonDrawOptions()
        {
        }
        public CommonDrawOptions(Rect rect, Color color)
        {
            Rect = rect;
            Color = color;
        }

        public Rect Rect { get; set; }
        public Color Color { get; set; }

        public virtual void SetupElement(TElement element)
        {
            element.SetRect(Rect);
            element.SetColor(Color);
        }

        public abstract IDrawOptions<TElement> CopyFrom(IDrawOptions<TElement> from);

        void IDrawableElement.SetColor(Color color)
        {
            Color = color;
        }

        void IRectElement.SetRect(Rect screenSpace)
        {
            Rect = screenSpace;
        }
    }
}