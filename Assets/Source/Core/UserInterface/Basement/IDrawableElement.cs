using UnityEngine;

namespace Core.UserInterface.Basement
{
    public interface IDrawableElement : IRectElement
    {
        void SetColor(Color color);
    }
}