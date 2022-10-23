using System;
using System.Collections.Generic;

public sealed class MainMenu : TemplateMenu
{
    protected override IEnumerable<(string label, Action action)> GetButtons()
    {
        yield return ("Play City Level", () => LevelManager.LoadLevel(2));
        yield return ("Play Mountains Level", () => LevelManager.LoadLevel(3));
        yield return ("Play Mountains Downhill Level", () => LevelManager.LoadLevel(4));
        if (MusicSource.MusicEnabled)
        {
            yield return ("Disable music in game", () => MusicSource.MusicEnabled = false);
        }
        else
        {
            yield return ("Enable music in game", () => MusicSource.MusicEnabled = true);
        }
        yield return ("Manual", () => NextMenu = new ManualMenu());
        yield return ("Exit", () => LevelManager.Quit());
    }
}
