using UnityEngine;

public sealed class MobileEmptyKey : IMobileKeyProvider
{
    public KeyCode KeyCode => KeyCode.None;
    public MobileKeyState State => MobileKeyState.None;
}
