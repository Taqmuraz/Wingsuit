using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class MobileJoystick : MonoBehaviour, IMobileControlProvider, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] float dragArea;
    [SerializeField] RectTransform outerHandle;
    [SerializeField] RectTransform innerHandle;
    Vector2 beginDragPoint;
    Vector2 input;
    CustomMobileAxis[] axes;
    CustomMobileKey[] keys;

    void Awake()
    {
        axes = new[] { new CustomMobileAxis("Horizontal", () => input.x), new CustomMobileAxis("Vertical", () => input.y) };
        keys = new[]
        {
            new CustomMobileKey(KeyCode.W, () => Mathf.Abs(input.y) > Mathf.Abs(input.x) && input.y > 0f ? MobileKeyState.Hold : MobileKeyState.None),
            new CustomMobileKey(KeyCode.A, () => Mathf.Abs(input.x) > Mathf.Abs(input.y) && input.x < 0f ? MobileKeyState.Hold : MobileKeyState.None),
            new CustomMobileKey(KeyCode.S, () => Mathf.Abs(input.y) > Mathf.Abs(input.x) && input.y < 0f ? MobileKeyState.Hold : MobileKeyState.None),
            new CustomMobileKey(KeyCode.D, () => Mathf.Abs(input.x) > Mathf.Abs(input.y) && input.x > 0f ? MobileKeyState.Hold : MobileKeyState.None),
        };
    }

    public IEnumerable<IMobileKeyProvider> GetKeys() => keys;
    public IEnumerable<IMobileAxisProvider> GetAxes() => axes;

    public void OnEndDrag(PointerEventData eventData)
    {
        input = new Vector2();
        innerHandle.anchoredPosition = new Vector2();
        outerHandle.anchoredPosition = new Vector2();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 relative = (eventData.position - beginDragPoint);
        relative = Vector3.ClampMagnitude(relative, dragArea);

        input = relative / dragArea;
        innerHandle.anchoredPosition = relative;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        outerHandle.position = beginDragPoint = eventData.position;
    }
}
