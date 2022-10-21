namespace Core.UserInterface.UnityUI
{
    using UnityEngine;
    using Core.UserInterface.Basement;

    public class UnityCanvasExtended : UnityCanvas, IExtendedCanvas
    {
        [SerializeField] Font defaultFont;
        [SerializeField] Color defaultTextColor = Color.black;
        [SerializeField] Texture2D defaultButtonImage;
        [SerializeField] Color defaultButtonColor;
        [SerializeField] Color defaultScrollbarBackgroundColor;
        [SerializeField] Color defaultScrollbarHandleColor;

        public static IExtendedCanvas CreateCanvas()
        {
            return Instantiate(ResourcesManager.LoadPrefab("Prefabs/UI/Canvas")).GetComponent<IExtendedCanvas>();
        }

        // TEXT

        public void DrawText(string text, Rect rect, Color color, Font font = null)
        {
            DrawText(new TextDrawOptions(text, font ?? defaultFont, rect, color));
        }
        public void DrawText(string text, Rect rect)
        {
            DrawText(new TextDrawOptions(text, defaultFont, rect, defaultTextColor));
        }
        public void DrawText(string text, Rect rect, Core.UserInterface.Basement.TextAlignment alignment)
        {
            DrawText(new TextDrawOptions(text, defaultFont, rect, defaultTextColor, alignment));
        }
        public void DrawText(string text, Rect rect, Color color, Core.UserInterface.Basement.TextAlignment alignment, Font font = null)
        {
            DrawText(new TextDrawOptions(text, font ?? defaultFont, rect, color, alignment));
        }

        // IMAGE

        public void DrawImage(Rect rect, Texture2D image)
        {
            DrawImage(new ImageDrawOptions(image, rect, Color.white));
        }
        public void DrawImage(Rect rect, Texture2D image, Color color)
        {
            DrawImage(new ImageDrawOptions(image, rect, color));
        }

        // BUTTON

        public bool DrawButton(string text, Rect rect, Font font = null)
        {
            return DrawButton(new ButtonDrawOptions(new TextDrawOptions(text, font ?? defaultFont, rect, defaultTextColor), new ImageDrawOptions(defaultButtonImage, rect, defaultButtonColor)));
        }
        public bool DrawButton(string text, Rect rect, Color textColor, Font font = null)
        {
            return DrawButton(new ButtonDrawOptions(new TextDrawOptions(text, font ?? defaultFont, rect, textColor), new ImageDrawOptions(defaultButtonImage, rect, defaultButtonColor)));
        }
        public bool DrawButton(string text, Rect rect, Texture2D image, Color textColor, Font font = null)
        {
            return DrawButton(new ButtonDrawOptions(new TextDrawOptions(text, font ?? defaultFont, rect, defaultTextColor * textColor), new ImageDrawOptions(image, rect, defaultButtonColor)));
        }
        public bool DrawButton(string text, Rect rect, Texture2D image, Color textColor, Color imageColor, Font font = null)
        {
            return DrawButton(new ButtonDrawOptions(new TextDrawOptions(text, font ?? defaultFont, rect, defaultTextColor * textColor), new ImageDrawOptions(image, rect, defaultButtonColor * imageColor)));
        }
        public bool DrawButton(string text, Rect rect, Color textColor, Color imageColor, Font font = null)
        {
            return DrawButton(new ButtonDrawOptions(new TextDrawOptions(text, font ?? defaultFont, rect, defaultTextColor * textColor), new ImageDrawOptions(defaultButtonImage, rect, defaultButtonColor * imageColor)));
        }

        // SCROLLBAR

        public float DrawHorizontalScrollbar(float value, Rect rect)
        {
            return DrawScrollbar(new ScrollbarDrawOptions(0, rect, defaultScrollbarHandleColor, defaultScrollbarBackgroundColor, rect.height, value));
        }

        // LINE

        public void DrawLine(Vector3 a, Vector3 b, Color color)
        {
            DrawLine(new LineDrawOptions(a, b, color, 0.1f));
        }
        public void DrawLine(Vector3 a, Vector3 b, Color color, float width)
        {
            DrawLine(new LineDrawOptions(a, b, color, width));
        }
        public void DrawLine(Vector3 a, Vector3 b, Color startColor, Color endColor)
        {
            DrawLine(new LineDrawOptions(a, b, startColor, endColor, 0.1f, 0.1f));
        }
        public void DrawLine(Vector3 a, Vector3 b, Color startColor, Color endColor, float startWidth, float endWidth)
        {
            DrawLine(new LineDrawOptions(a, b, startColor, endColor, startWidth, endWidth));
        }
    }
}