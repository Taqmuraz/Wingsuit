using System;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    static int deferredLevel = 0;

    public static void LoadLevel(int index)
    {
        deferredLevel = index;
        SceneManager.LoadScene(1);
    }
    public static void LoadDeferredLevel()
    {
        SceneManager.LoadScene(deferredLevel);
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }
}
