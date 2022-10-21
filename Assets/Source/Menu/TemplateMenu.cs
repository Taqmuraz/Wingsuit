using Core.UserInterface.Basement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TemplateMenu : IMenu
{
    protected abstract IEnumerable<(string label, Action action)> GetButtons();
    protected IMenu NextMenu { get; set; }

    public TemplateMenu()
    {
        NextMenu = this;
    }

    public IMenu DrawMenu(IExtendedCanvas canvas)
    {
        var buttons = GetButtons().Reverse().ToArray();
        float elementWidth = 200f;
        float elementHeight = 50f;
        float startX = (Screen.width - elementWidth) * 0.5f;
        float startY = (Screen.height - elementHeight * buttons.Length) * 0.5f;
        for (int i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];
            if (canvas.DrawButton(button.label, new Rect(startX, startY + elementHeight * i, elementWidth, elementHeight)))
            {
                button.action();
            }
        }
        return NextMenu;
    }
}
