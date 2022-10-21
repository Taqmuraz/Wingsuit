namespace Core.UserInterface.UnityUI
{
    using UnityEngine.UI;
    using Core.UserInterface.Basement;
    using Core.Abstractions;
    using UnityEngine;

    public sealed class ImageElement : RawImage, IImage, IReleasable
    {
        void IImage.SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        void IDrawableElement.SetColor(Color color)
        {
            this.color = color;
        }

        void IRectElement.SetRect(Rect rect)
        {
            rectTransform.SetRect(rect);
        }

        void IReleasable.Release()
        {
            Destroy(gameObject);
        }
    }
}