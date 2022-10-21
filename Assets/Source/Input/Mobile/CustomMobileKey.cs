using System;
using UnityEngine;

public sealed class CustomMobileKey : IMobileKeyProvider
{
    Func<MobileKeyState> stateGetter;

    public CustomMobileKey(KeyCode keyCode, Func<MobileKeyState> stateGetter)
    {
        KeyCode = keyCode;
        this.stateGetter = stateGetter;
    }

    public KeyCode KeyCode { get; }
    public MobileKeyState State => stateGetter();
}
