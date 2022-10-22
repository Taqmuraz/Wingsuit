using Core.UserInterface.Basement;
using UnityEngine;

public sealed class ManualMenu : IMenu
{
    string text;

    public ManualMenu()
    {
        text = Resources.Load<TextAsset>("Manual").text;
    }

    public IMenu DrawMenu(IExtendedCanvas canvas)
    {
        Vector2 s = new Vector2(Screen.width, Screen.height);
        canvas.DrawText(text, new Rect(s.x * 0.1f, s.y * 0.1f, s.x * 0.8f, s.y * 0.8f));
        if (canvas.DrawButton("To main menu", new Rect(s.x * 0.4f, 0f, s.x * 0.2f, s.y * 0.1f)))
        {
            return new MainMenu();
        }
        else return this;
    }
}