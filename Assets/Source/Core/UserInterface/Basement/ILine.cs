using UnityEngine;

namespace Core.UserInterface.Basement
{
    public interface ILine : IElement
    {
        void SetPoints(Vector3 a, Vector3 b);
        void SetWidth(float startWidth, float endWidth);
        void SetColor(Color startColor, Color endColor);
    }
}