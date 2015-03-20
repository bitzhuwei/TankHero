using UnityEngine;
using System.Collections;

public class SelectMapConfig
{
    public System.Collections.Generic.List<string> warProgressList = new System.Collections.Generic.List<string>();
    static readonly char[] pairsSeparator = new char[] { ';', '\r', '\n', '\t' };

    public static SelectMapConfig Parse(string content)
    {
        var result = new SelectMapConfig();
        var pairs = content.Split(pairsSeparator, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (var pair in pairs)
        {
            result.warProgressList.Add(pair);
        }
        return result;
    }

    public void Save(string relativeFile)
    {
        var builder = new System.Text.StringBuilder();
        foreach (var item in warProgressList)
        {
            builder.AppendFormat("{0};{1}", item, System.Environment.NewLine);
        }
        FileHelper.Write(relativeFile, builder.ToString());
    }

    public override string ToString()
    {
        var builder = new System.Text.StringBuilder();
        foreach (var item in warProgressList)
        {
            builder.AppendFormat("{0};{1}", item, System.Environment.NewLine);
        }
        return builder.ToString();
    }

    public string NextEpisode()
    {
        if (warProgressList.Count == 0)
        { return "episode1"; }

        var last = this.warProgressList[this.warProgressList.Count - 1];
        if (last.Contains("episode13"))
        { return "episode13"; }
        else
        {
            if (last.Contains("map5"))
            {
                var parts = last.Split(new string[] { "episode", "map" }, 1, System.StringSplitOptions.RemoveEmptyEntries);
                return string.Format("episode{0}",
                    int.Parse(parts[0]) + 1);
            }
            else
            { return last.Substring(0, last.IndexOf("map")); }
        }
    }

    public string NextMap()
    {
        if (warProgressList.Count == 0)
        { return "map1"; }

        var last = this.warProgressList[this.warProgressList.Count - 1];
        if (last.Contains("episode13"))
        { return "map1"; }
        else
        {
            var parts = last.Split(new string[] { "episode", "map" }, System.StringSplitOptions.RemoveEmptyEntries);
            var index = int.Parse(parts[1]) + 1;
            if (index == 6)
            { index = 1; }
            return string.Format("map{0}", index);
        }
    }
}