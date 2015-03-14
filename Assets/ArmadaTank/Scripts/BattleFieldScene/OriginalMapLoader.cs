using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OriginalMapLoader : MonoBehaviour
{
    public OriginalMap map;
    private static Dictionary<string, string> episodeDict = new Dictionary<string, string>();
    //private List<GameObject> objects;

    public static string GetEpisodeName(string key)
    {
        var result = string.Empty;
        if (episodeDict.TryGetValue(key, out result))
        {
            return result;
        }
        else
        {
            Debug.LogError(string.Format("episode key [{0}] not exists!", key));
            return episodeDict["episode1"]; 
        }
    }
    public static string GetEpisodeName(int index)
    {
        InitializeEpisodeDict();
        if(0<index&&index<14)
        {
            return episodeDict[string.Format("episode{0}", index)];
        }
        else
        {
            Debug.LogError(string.Format("episode [{0}] not exists!", index));
            return episodeDict["episode1"];
        }
    }
    void Awake()
    {
        InitializeEpisodeDict();
    }

    private static void InitializeEpisodeDict()
    {
        if (episodeDict.Count == 0)
        {
            episodeDict.Add("episode1", "RUIN");
            episodeDict.Add("episode2", "SAND");
            episodeDict.Add("episode3", "RAIN");
            episodeDict.Add("episode4", "SNOW");
            episodeDict.Add("episode5", "EGYPT");
            episodeDict.Add("episode6", "ICE");
            episodeDict.Add("episode7", "FIRE");
            episodeDict.Add("episode8", "TUND");
            episodeDict.Add("episode9", "CAST");
            episodeDict.Add("episode10", "DUNG");
            episodeDict.Add("episode11", "GROT");
            episodeDict.Add("episode12", "FARO");
            episodeDict.Add("episode13", "END");
        }
    }
    // Use this for initialization
    void Start()
    {
        var selectMapManager = GameObject.FindGameObjectWithTag(Tags.SelectMapManager);
        var manager = selectMapManager.GetComponent<SelectMapManager>();
        BuildBattleField(manager);
    }

    private void BuildBattleField(SelectMapManager manager)
    {
        //var path = FileHelper.GetPath();
        var filename = string.Format(@"maps/{0}/m{1}",
            GetEpisodeName(manager.selectedEpisode),
            manager.selectedMap[manager.selectedMap.Length - 1]);
        this.map = OriginalMap.GetOriginalMap(filename);
        //this.objects = this.map.Build();
        this.map.Build();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
