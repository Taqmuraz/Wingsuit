using UnityEngine;

namespace Core.UserInterface.Basement
{
    public interface IPoint : IWorldSpaceGeometryElement
    {
        void SetPosition(Vector3 position);
        void SetRadius(float radius);
    }
}