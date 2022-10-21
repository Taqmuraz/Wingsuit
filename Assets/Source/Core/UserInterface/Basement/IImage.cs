using UnityEngine;

namespace Core.UserInterface.Basement
{
    public interface IImage : IDrawableElement
    {
        void SetTexture(Texture2D texture);
    }
}