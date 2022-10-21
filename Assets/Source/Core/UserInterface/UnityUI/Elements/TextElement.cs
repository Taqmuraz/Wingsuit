namespace Core.UserInterface.UnityUI
{
    using UnityEngine.UI;
    using Core.UserInterface.Basement;
    using UnityEngine;
    using Core.Abstractions;

    public sealed class TextElement : Text, IText, IReleasable
    {
        void IText.SetText(string text)
        {
            this.text = text;
        }

        void IText.SetFont(Font font)
        {
            this.font = font;
            var fontSize = (int)SettingsManager.Settings.ReadValue<float>("FontSize");
            this.fontSize = fontSize;
            resizeTextMaxSize = fontSize;
            resizeTextForBestFit = true;
        }

        void IDrawableElement.SetColor(Color color)
        {
            this.color = color;
        }

        void IRectElement.SetRect(Rect screenSpace)
        {
            rectTransform.SetRect(screenSpace);
        }

        void IReleasable.Release()
        {
            Destroy(gameObject);
        }

        void IText.SetAlignment(Basement.TextAlignment alignment)
        {
            this.alignment = (TextAnchor)(int)alignment;
        }
    }
}