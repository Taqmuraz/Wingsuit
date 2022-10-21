using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileManager
{
    public static string RootPath => Application.persistentDataPath;

    public static void SaveFile<TArg>(string fileName, TArg arg)
    {
        try
        {
            using (FileStream stream = File.OpenWrite(RootPath + "/" + fileName))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, arg);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    public static TResult LoadFile<TResult>(string fileName)
    {
        try
        {
            string path = RootPath + "/" + fileName;
            if (!File.Exists(path)) return default(TResult);

            using (FileStream stream = File.OpenRead(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (TResult)formatter.Deserialize(stream);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return default(TResult);
        }
    }
    public static bool IsFileExists(string fileName)
    {
        return File.Exists(RootPath + "/" + fileName);
    }
}