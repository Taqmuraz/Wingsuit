using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class MobileInputProvider : MonoBehaviour, IInputProvider
{
    static IEnumerable<IMobileAxisProvider> emptyAxis = new[] { new MobileEmptyAxis() };
    static IEnumerable<IMobileKeyProvider> emptyKey = new[] { new MobileEmptyKey() };
    public static IInputProvider Instance { get; private set; }

    SafeDictionary<string, IEnumerable<IMobileAxisProvider>> axes = new SafeDictionary<string, IEnumerable<IMobileAxisProvider>>(() => emptyAxis);
    SafeDictionary<KeyCode, IEnumerable<IMobileKeyProvider>> keys = new SafeDictionary<KeyCode, IEnumerable<IMobileKeyProvider>>(() => emptyKey);

    void Start()
    {
#if UNITY_MOBILE
        var controls = GetComponentsInChildren<IMobileControlProvider>();
        foreach (var axisGroup in controls.SelectMany(c => c.GetAxes()).GroupBy(a => a.Name))
        {
            axes.Add(axisGroup.Key, axisGroup.ToArray());
        }
        foreach (var keyGroup in controls.SelectMany(c => c.GetKeys()).GroupBy(k => k.KeyCode))
        {
            keys.Add(keyGroup.Key, keyGroup.ToArray());
        }
        Instance = this;
#else
        gameObject.SetActive(false);
#endif
    }

    bool IInputProvider.GetKey(KeyCode keyCode)
    {
        return keys[keyCode].Any(k => k.State != MobileKeyState.None);
    }

    bool IInputProvider.GetKeyDown(KeyCode keyCode)
    {
        return keys[keyCode].Any(k => k.State == MobileKeyState.Down);
    }

    bool IInputProvider.GetKeyUp(KeyCode keyCode)
    {
        return keys[keyCode].Any(k => k.State == MobileKeyState.Up);
    }

    float IInputProvider.GetAxis(string name)
    {
        var axis = axes[name].FirstOrDefault(a => a.Value != 0);
        return axis == null ? 0f : axis.Value;
    }
}