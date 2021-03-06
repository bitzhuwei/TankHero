﻿using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public class SelectMapManager : MonoBehaviour {

    //private System.Collections.Generic.List<string> warProgressList;
    public string selectedEpisode;
    public string selectedMap;
    private string defaultSelectMapConfig ="";// @"episode1map1;";
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private SelectMapConfig config;
    // Use this for initialization
    void Start()
    {
        //warProgressList = new System.Collections.Generic.List<string>();
        var content = FileHelper.Read(ConfigFilenames.SelectMapConfig);
        if (content == string.Empty)
        {
            FileHelper.Write(ConfigFilenames.SelectMapConfig, defaultSelectMapConfig);
            content = defaultSelectMapConfig;
        }
        this.config = SelectMapConfig.Parse(content);
        this.selectedEpisode = this.config.NextEpisode();// "episode1";
        this.selectedMap = this.config.NextMap();// "map1";
    }

    // Update is called once per frame
    void Update()
    {

    }


    public bool IsPassed(string episodeName)
    {
        for (int i = 0; i < 5; i++)
        {
            var key = string.Format("{0}map{1}",
                episodeName, (i + 1));
            if (!this.config.warProgressList.Contains(key))
            {
                { return false; }
            }
        }
        return true;
    }

    public bool IsPlayable(string mapName)
    {
        {
            var key = string.Format("{0}{1}", selectedEpisode, mapName);
            if (this.config.warProgressList.Contains(key))
            {
                return true;
            }
        }

        var episodeCount = int.Parse(selectedEpisode.Substring("episode".Length));
        for (int i = 1; i < episodeCount; i++)
        {
            var episode = string.Format("episode{0}", i);
            if (!IsPassed(episode))
            { return false; }
        }
        var mapCount = int.Parse(mapName.Substring("map".Length));
        for (int i = 1; i < mapCount; i++)
        {
            var key = string.Format("{0}map{1}", selectedEpisode, i);
            if(!this.config.warProgressList.Contains(key))
            { return false; }
        }

        return true;
    }
}
