using UnityEngine;

namespace Core.UserInterface.Basement
{
    public interface IRectElement : IElement
    {
        void SetRect(Rect screenSpace);
    }
}