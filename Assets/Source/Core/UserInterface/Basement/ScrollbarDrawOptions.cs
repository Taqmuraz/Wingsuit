namespace Core.UserInterface.Basement
{
    using UnityEngine;

    public sealed class ScrollbarDrawOptions : IDrawOptions<IScrollbar>, IScrollbar
    {
        public Rect Rect { get; set; }
        public Color HandleColor { get; set; }
        public Color BackgroundColor { get; set; }
        public ScrollbarOrientation ScrollbarOrientation { get; set; }
        public float HandleSize { get; set; }
        public float Value { get; set; }

        public ScrollbarDrawOptions()
        {
        }

        public ScrollbarDrawOptions(ScrollbarOrientation scrollbarOrientation, Rect rect, Color toggleColor, Color backgroundColor, float handleSize, float value)
        {
            ScrollbarOrientation = scrollbarOrientation;
            Rect = rect;
            HandleColor = toggleColor;
            BackgroundColor = backgroundColor;
            HandleSize = handleSize;
            Value = value;
        }

        public void SetupElement(IScrollbar element)
        {
            element.SetHandleColor(HandleColor);
            element.SetBackgroundColor(BackgroundColor);
            element.SetOrientation(ScrollbarOrientation);
            element.HandleSize = HandleSize;
            element.Value = Value;
            element.SetRect(Rect);
        }

        public IDrawOptions<IScrollbar> CopyFrom(IDrawOptions<IScrollbar> from)
        {
            from.SetupElement(this);
            return this;
        }

        public void SetOrientation(ScrollbarOrientation orientation)
        {
            ScrollbarOrientation = orientation;
        }

        public void SetHandleColor(Color color)
        {
            HandleColor = color;
        }

        public void SetBackgroundColor(Color color)
        {
            BackgroundColor = color;
        }

        public void SetRect(Rect screenSpace)
        {
            Rect = screenSpace;
        }
    }
}