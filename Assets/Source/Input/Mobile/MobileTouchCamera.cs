using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class MobileTouchCamera : MonoBehaviour, IMobileControlProvider, IDragHandler
{
    IEnumerable<IMobileAxisProvider> axes;
    Vector2 input;
    [SerializeField] float dragUnit = 150f;
    MobileKeyState state;

    void Awake()
    {
        axes = new[] { new CustomMobileAxis("Mouse X", () => input.x), new CustomMobileAxis("Mouse Y", () => input.y) };
    }

    public IEnumerable<IMobileKeyProvider> GetKeys()
    {
        yield break;
    }
    public IEnumerable<IMobileAxisProvider> GetAxes() => axes;

    void Update()
    {
        switch (state)
        {
            case MobileKeyState.None:
                input = new Vector2();
                break;
            case MobileKeyState.Down:
                state = MobileKeyState.Hold;
                break;
            case MobileKeyState.Hold:
                state = MobileKeyState.Up;
                break;
            case MobileKeyState.Up:
                state = MobileKeyState.None;
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        input = eventData.delta / dragUnit;
        state = MobileKeyState.Down;
    }
}
