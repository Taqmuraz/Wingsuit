namespace Core.UserInterface.UnityUI
{
    using Core.UserInterface.Basement;
    using UnityEngine;
    using Core.Abstractions;

    public sealed class ButtonElement : MonoBehaviour, IButton, IReleasable
    {
        [SerializeField] TextElement text;
        [SerializeField] ImageElement image;

        void IButton.SetupText(IDrawOptions<IText> textOptions)
        {
            textOptions.SetupElement(text);
        }

        void IButton.SetupImage(IDrawOptions<IImage> imageOptions)
        {
            imageOptions.SetupElement(image);
        }

        void IReleasable.Release()
        {
            Destroy(gameObject);
        }
    }
}