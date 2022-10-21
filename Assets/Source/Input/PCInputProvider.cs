using UnityEngine;

public sealed class PCInputProvider : IInputProvider
{
    public static PCInputProvider Instance { get; } = new PCInputProvider();

    public bool GetKey(KeyCode keyCode)
    {
        return Input.GetKey(keyCode);
    }

    public bool GetKeyDown(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }

    public bool GetKeyUp(KeyCode keyCode)
    {
        return Input.GetKeyUp(keyCode);
    }

    public float GetAxis(string name)
    {
        switch (name)
        {
            case "Horizontal":
                float value = 0f;
                if (Input.GetKey(KeyCode.A)) value -= 1f;
                if (Input.GetKey(KeyCode.D)) value += 1f;
                return value;
            case "Vertical":
                value = 0f;
                if (Input.GetKey(KeyCode.S)) value -= 1f;
                if (Input.GetKey(KeyCode.W)) value += 1f;
                return value;
        }
        return Input.GetAxis(name);
    }
}