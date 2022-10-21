using UnityEngine;

namespace Core.UserInterface.Basement
{
    public interface IText : IDrawableElement
    {
        void SetText(string text);
        void SetFont(Font font);
        void SetAlignment(TextAlignment alignment);
    }
}