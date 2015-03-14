using UnityEngine;
using System.Collections;
using System.IO;

public class FileHelper
{
    public static string GetPath()
    {
        return path;
    }
    static string path = string.Empty;
    static FileHelper()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
        }
        else
        {
            path = Application.dataPath;
        }
    }
    public static string Read(string relativeFile)
    {
        var result = LoadFile(path, relativeFile);
        return result;
    }
    private static string LoadFile(string path, string name)
    {
        FileInfo fileInfo = new FileInfo(path + @"/" + name);
        if (!fileInfo.Exists) { return string.Empty; }
        var result = string.Empty;
        using (var reader = new StreamReader(fileInfo.FullName))
        {
            result = reader.ReadToEnd();
        }
        return result;
    }

    public static void Write(string relativeFile, string content)
    {
        CreateOrWrite(path, relativeFile, content);
    }
    private static void CreateOrWrite(string path, string name, string content)
    {
        FileInfo fileInfo = new FileInfo(path + @"/" + name);
        if (!fileInfo.Directory.Exists)
        { Directory.CreateDirectory(fileInfo.DirectoryName); }

        using (var writer = new StreamWriter(fileInfo.FullName))
        {
            writer.Write(content);
        }
    }

    public static void DeleteFile(string path, string name)
    {
        File.Delete(path + @"/" + name);
    }
}
