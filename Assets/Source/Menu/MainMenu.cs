using Core.UserInterface.Basement;
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class MainMenu : TemplateMenu
{
    protected override IEnumerable<(string label, Action action)> GetButtons()
    {
        yield return ("Play", () => LevelManager.LoadLevel(2));
        yield return ("Settings", () => NextMenu = new SettingsMenu());
        yield return ("Exit", () => LevelManager.Quit());
    }
}
