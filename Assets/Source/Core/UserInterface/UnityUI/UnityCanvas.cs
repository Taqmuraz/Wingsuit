namespace Core.UserInterface.UnityUI
{
    using UnityEngine;
    using Core.UserInterface.Basement;
    using Core.Abstractions;

    public class UnityCanvas : OperationsBasedCanvas<string>
    {
        static readonly string prefabsPath = "Prefabs/UI/";

        protected override TElement CreateElement<TElement>(string prefabName)
        {
            var resource = ResourcesManager.LoadPrefab(prefabsPath + prefabName);
            var instance = Instantiate(resource, transform);
            return instance.GetComponent<TElement>();
        }

        protected override void HandleElement<TElement>(IElement element, IDrawOptions<TElement> options)
        {
            var transform = (element as MonoBehaviour)?.transform;
            transform.SetSiblingIndex(transform.parent.childCount - 1);
            base.HandleElement(element, options);
        }

        protected bool IsMousePress()
        {
            return Input.GetMouseButtonDown(0);
        }
        protected bool IsMouseHold()
        {
            return Input.GetMouseButton(0);
        }
        protected bool IsMouseHover(Rect rect)
        {
            return rect.Contains(Input.mousePosition);
        }
        protected Vector2 MousePosition => Input.mousePosition;

        public override bool DrawButton(IDrawOptions<IButton> options)
        {
            var buttonOptions = options.CloneOptions<ButtonDrawOptions, IButton>();
            var imageOptions = buttonOptions.Image.CloneOptions<ImageDrawOptions, IImage>();
            var textOptions = buttonOptions.Text.CloneOptions<TextDrawOptions, IText>();

            bool isMouseHover = IsMouseHover(imageOptions.Rect);
            bool isMouseDown = IsMousePress();
            
            if (isMouseHover)
            {
                imageOptions.Color *= new Color(0.5f, 1f, 0.5f, 1f);
                textOptions.Color *= new Color(0.5f, 1f, 0.5f, 1f);
            }

            DrawElement(new ButtonDrawOptions(textOptions, imageOptions), "Button");

            return isMouseHover && isMouseDown;
        }

        public override float DrawScrollbar(IDrawOptions<IScrollbar> scrollbarOptions)
        {
            ScrollbarDrawOptions options = scrollbarOptions.CloneOptions<ScrollbarDrawOptions, IScrollbar>();
            bool hover = IsMouseHover(options.Rect);
            bool hold = IsMouseHold();

            if (hover && hold)
            {
                int orientation = (int)options.ScrollbarOrientation;
                Vector2 delta = MousePosition - new Vector2(options.Rect.x + options.HandleSize * 0.5f, options.Rect.y);
                options.Value = Mathf.Clamp01(delta[orientation] / (options.Rect.size[orientation] - options.HandleSize));
            }

            DrawElement(options, "Scrollbar");
            return options.Value;
        }

        public override void DrawText(IDrawOptions<IText> textOptions)
        {
            DrawElement(textOptions, "Text");
        }

        public override void DrawImage(IDrawOptions<IImage> imageOptions)
        {
            DrawElement(imageOptions, "Image");
        }

        public override void DrawLine(IDrawOptions<ILine> lineOptions)
        {
            DrawElement(lineOptions, "Line");
        }

        public override void DrawPoint(IDrawOptions<IPoint> pointOptions)
        {
            DrawElement(pointOptions, "Point");
        }

        public override void Release()
        {
            if (this) Destroy(gameObject);
        }
    }
}