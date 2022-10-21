using UnityEngine;

namespace Core.UserInterface.Basement
{
    public sealed class TextDrawOptions : CommonDrawOptions<IText>, IText
    {
        public string Text { get; set; }
        public Font Font { get; set; }
        public TextAlignment Alignment { get; set; }

        public TextDrawOptions()
        {
        }

        public TextDrawOptions(string text, Font font, Rect rect, Color color) : base(rect, color)
        {
            Text = text;
            Font = font;
            Alignment = TextAlignment.MiddleCenter;
        }
        public TextDrawOptions(string text, Font font, Rect rect, Color color, TextAlignment alignment) : base(rect, color)
        {
            Text = text;
            Font = font;
            Alignment = alignment;
        }

        public override void SetupElement(IText text)
        {
            base.SetupElement(text);
            text.SetAlignment(Alignment);
            text.SetText(Text ?? string.Empty);
            if (Font != null) text.SetFont(Font);
        }

        public override IDrawOptions<IText> CopyFrom(IDrawOptions<IText> from)
        {
            from.SetupElement(this);
            return this;
        }

        void IText.SetText(string text)
        {
            Text = text;
        }

        void IText.SetFont(Font font)
        {
            Font = font;
        }

        public void SetAlignment(TextAlignment alignment)
        {
            Alignment = alignment;
        }
    }
}