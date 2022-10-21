using UnityEngine;

namespace Core.UserInterface.Basement
{
    public interface IExtendedCanvas : Basement.IUserInterfaceCanvas
    {
        bool DrawButton(string text, Rect rect, Font font = null);
        bool DrawButton(string text, Rect rect, Color textColor, Font font = null);
        bool DrawButton(string text, Rect rect, Color textColor, Color imageColor, Font font = null);
        bool DrawButton(string text, Rect rect, Texture2D image, Color textColor, Font font = null);
        bool DrawButton(string text, Rect rect, Texture2D image, Color textColor, Color imageColor, Font font = null);
        float DrawHorizontalScrollbar(float value, Rect rect);
        void DrawImage(Rect rect, Texture2D image);
        void DrawImage(Rect rect, Texture2D image, Color color);
        void DrawText(string text, Rect rect);
        void DrawText(string text, Rect rect, Color color, Font font = null);
        void DrawText(string text, Rect rect, Color color, Basement.TextAlignment alignment, Font font = null);
        void DrawText(string text, Rect rect, Basement.TextAlignment alignment);
        void DrawLine(Vector3 a, Vector3 b, Color color);
        void DrawLine(Vector3 a, Vector3 b, Color color, float width);
        void DrawLine(Vector3 a, Vector3 b, Color startColor, Color endColor);
        void DrawLine(Vector3 a, Vector3 b, Color startColor, Color endColor, float startWidth, float endWidth);
    }
}