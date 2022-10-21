using System;
using System.Collections.Generic;

public sealed class SettingsMenu : TemplateMenu
{
    protected override IEnumerable<(string label, Action action)> GetButtons()
    {
        yield return ("Calcel", () => NextMenu = new MainMenu());
    }
}
