using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class MobileButtonAxis : MonoBehaviour, IMobileControlProvider, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] string axisName;
    [SerializeField] float pressValue = 1f;
    [SerializeField] float defaultValue = 0f;
    CustomMobileAxis axis;
    float currentValue;

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
        currentValue = pressValue;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        currentValue = defaultValue;
    }
}
