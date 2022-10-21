using UnityEngine;

public interface IMobileKeyProvider
{
    KeyCode KeyCode { get; }
    MobileKeyState State { get; }
}
