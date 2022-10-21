namespace Core.UserInterface.Basement
{
    using UnityEngine;

    public interface IScrollbar : IRectElement
    {
        void SetOrientation(ScrollbarOrientation orientation);
        void SetHandleColor(Color color);
        void SetBackgroundColor(Color color);
        float HandleSize { get; set; }
        float Value { get; set; }
    }
}