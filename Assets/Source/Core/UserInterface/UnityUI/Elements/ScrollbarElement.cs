namespace Core.UserInterface.UnityUI
{
    using Core.UserInterface.Basement;
    using UnityEngine;
    using Core.Abstractions;

    public sealed class ScrollbarElement : MonoBehaviour, IScrollbar, IReleasable
    {
        int orientation;
        IImage handle;
        IImage background;

        void Start()
        {
            background = transform.Find("Background").GetComponent<IImage>();
            handle = transform.Find("Handle").GetComponent<IImage>();
        }

        void IScrollbar.SetOrientation(ScrollbarOrientation orientation)
        {
            this.orientation = (int)orientation;
        }

        void IScrollbar.SetHandleColor(Color color)
        {
            handle.SetColor(color);
        }

        void IScrollbar.SetBackgroundColor(Color color)
        {
            background.SetColor(color);
        }

        public float Value { get; set; }
        public float HandleSize { get; set; }

        void IRectElement.SetRect(Rect screenSpace)
        {
            background.SetRect(screenSpace);
            Vector2 begin = new Vector2(screenSpace.x, screenSpace.y);
            int opposite = (orientation + 1) & 1;
            float handleSizeX = HandleSize;
            float handleSizeY = screenSpace.size[opposite];
            Vector2 dir = new Vector2() { [orientation] = screenSpace.size[orientation] - handleSizeX };
            Rect handleRect = new Rect(begin + dir * Value, new Vector2() { [orientation] = handleSizeX, [opposite] = handleSizeY });
            handle.SetRect(handleRect);
        }

        void IReleasable.Release()
        {
            Destroy(gameObject);
        }
    }
}