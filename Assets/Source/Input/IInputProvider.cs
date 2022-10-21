using UnityEngine;

public interface IInputProvider
{
    bool GetKey(KeyCode keyCode);
    bool GetKeyDown(KeyCode keyCode);
    bool GetKeyUp(KeyCode keyCode);
    float GetAxis(string name);
}
