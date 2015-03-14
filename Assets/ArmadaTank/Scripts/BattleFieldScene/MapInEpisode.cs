using UnityEngine;
using System.Collections;

public class MapInEpisode
{
    public Episode episode;
    public int index;
    public MapInEpisode(Episode episode)
    {
        this.episode = episode;
    }
    public override string ToString()
    {
        return string.Format("{0}map{1}", episode, index);
        //return base.ToString();
    }
}