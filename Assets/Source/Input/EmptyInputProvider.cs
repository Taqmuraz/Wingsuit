using UnityEngine;

public sealed class EmptyInputProvider : IInputProvider
{
    public bool GetKey(KeyCode keyCode) => false;
    public bool GetKeyDown(KeyCode keyCode) => false;
    public bool GetKeyUp(KeyCode keyCode) => false;
    public float GetAxis(string name) => 0f;
}
