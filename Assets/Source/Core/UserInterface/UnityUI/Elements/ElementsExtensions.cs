namespace Core.UserInterface.UnityUI
{
    using UnityEngine;

    public static class ElementsExtensions
    {
        public static void SetRect(this RectTransform rectTransform, Rect rect)
        {
            rectTransform.anchorMin = rectTransform.anchorMax = Vector2.zero;
            rectTransform.pivot = Vector2.zero;
            rectTransform.anchoredPosition = rect.min;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.size.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.size.y);
        }
    }
}