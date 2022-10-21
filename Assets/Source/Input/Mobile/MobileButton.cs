using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class MobileButton : MonoBehaviour, IMobileControlProvider, IMobileKeyProvider, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] KeyCode keyCode;
    public KeyCode KeyCode => keyCode;

    public MobileKeyState State { get; private set; }

    void LateUpdate()
    {
        switch (State)
        {
            case MobileKeyState.Down:
                State = MobileKeyState.Hold;
                break;
            case MobileKeyState.Up:
                State = MobileKeyState.None;
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        State = MobileKeyState.Down;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        State = MobileKeyState.Up;
    }

    public IEnumerable<IMobileKeyProvider> GetKeys()
    {
        yield return this;
    }

    public IEnumerable<IMobileAxisProvider> GetAxes()
    {
        yield break;
    }
}
