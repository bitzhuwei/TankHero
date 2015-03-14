using UnityEngine;
using System.Collections;
using System.IO;

public class ReadWriteTest : MonoBehaviour {
    public UnityEngine.UI.Text lblInfo;
    public UnityEngine.UI.Text lblPath;

    public string testFilename = @"config/ReadWriteTest.txt";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
            
	}

    public void DoTest()
    {
        ReadFile();
        WriteFile();
    }
    private void WriteFile()
    {
        string path = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = Application.dataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            path = Application.dataPath;
        }

        string configip = LoadFile(path, testFilename);
        if (configip != "error")
        {
            createORwriteConfigFile(path, testFilename, (int.Parse(configip) + 1).ToString());
        }
        else
        {
            createORwriteConfigFile(path, testFilename, "0");
        }
    }

    private void ReadFile()
    {
        string path = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = Application.dataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            path = Application.dataPath;
        }

        string configip = LoadFile(path, testFilename);
        if (configip != "error")
        {
            lblInfo.text = "read: " + configip;
        }
        else
        {
            createORwriteConfigFile(path, testFilename, "0");
            configip = LoadFile(path, testFilename);
            lblInfo.text = "read: " + configip;
        }
    }
    /// <summary>
    /// 在指定位置创建文件   如果文件已经存在则追加文件内容
    /// </summary>
    /// <param name='path'>
    /// 路径
    /// </param>
    /// <param name='name'>
    /// 文件名
    /// </param>
    /// <param name='info'>
    /// 文件内容
    /// </param>
    private void createORwriteConfigFile(string path, string name, string info)
    {
        //StreamWriter sw;
        FileInfo t = new FileInfo(path + @"/" + name);
        if(!t.Directory.Exists)
        { Directory.CreateDirectory(t.DirectoryName); }
        var builder = new System.Text.StringBuilder();
        builder.AppendLine(path);
        builder.AppendLine(name);
        builder.AppendLine(path + @"/" + name);
        foreach (var item in Directory.GetFiles(path,"*", SearchOption.AllDirectories))
        {
            builder.Append("["); builder.Append(item); builder.Append("]");    
        }
        lblPath.text = builder.ToString();
        using (var writer = new StreamWriter(path+@"/"+name))
        {
            writer.Write(info);
        }
        //if (!t.Exists)
        //{
        //    sw = t.CreateText();
        //}
        //else
        //{
        //    sw = t.AppendText();
        //}
        //sw.Write(info);
        //sw.Close();
        //sw.Dispose();
    }
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name='path'>
    /// Path.
    /// </param>
    /// <param name='name'>
    /// Name.
    /// </param>
    void DeleteFile(string path, string name)
    {
        File.Delete(path + "//" + name);
    }
    /// <summary>
    /// 读取文件内容  仅读取第一行
    /// </summary>
    /// <param name='path'>
    /// Path.
    /// </param>
    /// <param name='name'>
    /// Name.
    /// </param>
    private string LoadFile(string path, string name)
    {
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            return "error";
        }
        StreamReader sr = null;
        sr = File.OpenText(path + "//" + name);
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            break;
        }
        sr.Close();
        sr.Dispose();
        return line;
    }      
}
