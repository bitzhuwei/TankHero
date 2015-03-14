using UnityEngine;
using System.Collections;

public class Episode : System.Collections.Generic.List<MapInEpisode>
{
   public new void Add(MapInEpisode item)
    {
        base.Add(item);
    }
}