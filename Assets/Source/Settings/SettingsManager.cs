using System;
using System.Collections.Generic;

[Serializable]
public sealed class SettingsManager
{
    static string FileName => "Settings.settings";

    static SettingsManager()
    {
        Settings = FileManager.LoadFile<SettingsManager>(FileName);
        if (Settings == null)
        {
            Settings = new SettingsManager();
            Settings.WriteValue("Quality", 1f);
            Settings.WriteValue("FontSize", 10f);
        }
    }

    public static SettingsManager Settings { get; }

    Dictionary<string, object> Values = new Dictionary<string, object>();

    public T ReadValue<T>(string name)
    {
        if (Values.ContainsKey(name))
        {
            var value = Values[name];
            if (value is T) return (T)value;
        }
        return default(T);
    }
    public void WriteValue<T>(string name, T value)
    {
        if (Values.ContainsKey(name)) Values[name] = value;
        else Values.Add(name, value);
    }

    public static void SaveChanges()
    {
        FileManager.SaveFile(FileName, Settings);
    }
}
