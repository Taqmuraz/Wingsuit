using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class MobileButtonAxis : MonoBehaviour, IMobileControlProvider, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] string axisName;
    [SerializeField] float pressValue = 1f;
    [SerializeField] float defaultValue = 0f;
    [SerializeField] bool smooth;
    CustomMobileAxis axis;
    float currentValue;
    bool pressed = false;

    void Awake()
    {
        axis = new CustomMobileAxis(axisName, () => currentValue);
    }

    public IEnumerable<IMobileKeyProvider> GetKeys()
    {
        yield break;
    }

    public IEnumerable<IMobileAxisProvider> GetAxes()
    {
        yield return axis;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        if (!smooth) currentValue = pressValue;
    }

    void Update()
    {
        if (pressed && smooth) currentValue = Mathf.Lerp(currentValue, pressValue, Time.deltaTime * 5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
        currentValue = defaultValue;
    }
}
