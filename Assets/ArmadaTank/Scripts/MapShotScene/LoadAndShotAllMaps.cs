using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadAndShotAllMaps : MonoBehaviour
{

    private List<GameObject> objects = new List<GameObject>();
    //private static Dictionary<string, string> episodeDict = new Dictionary<string, string>();

    void Awake()
    {
        //if (episodeDict.Count == 0)
        //{
        //    episodeDict.Add("episode1", "RUIN");
        //    episodeDict.Add("episode2", "SAND");
        //    episodeDict.Add("episode3", "RAIN");
        //    episodeDict.Add("episode4", "SNOW");
        //    episodeDict.Add("episode5", "EGYPT");
        //    episodeDict.Add("episode6", "ICE");
        //    episodeDict.Add("episode7", "FIRE");
        //    episodeDict.Add("episode8", "TUND");
        //    episodeDict.Add("episode9", "CAST");
        //    episodeDict.Add("episode10", "DUNG");
        //    episodeDict.Add("episode11", "GROT");
        //    episodeDict.Add("episode12", "FARO");
        //    episodeDict.Add("episode13", "END");
        //}
    }
    // Use this for initialization
    void Start()
    {
    }



    int i = 1;
    int j = 1;
    bool building = true;
    int initializationCount = 0;
    // Update is called once per frame
    void Update()
    {
        initializationCount++;
        if (initializationCount < 10) { return; }
        if (i == 13 && j == 2) { return; }

        try
        {
            if (building)
            {
                var filename = string.Format(@"maps/{0}/m{1}",
                    OriginalMapLoader.GetEpisodeName(i),
                    //episodeDict[("episode" + i)], 
                    j);
                var mapObj = OriginalMap.GetOriginalMap(filename);
                objects = mapObj.Build();
                Application.CaptureScreenshot(string.Format(@"MapShot/episode{0}m{1}({2}).map.jpg",
                    i, j, OriginalMapLoader.GetEpisodeName(i)));// episodeDict[("episode" + i)]));
                building = false;
            }
            else
            {
                for (int k = 0; k < objects.Count; k++)
                {
                    Destroy(objects[k]);
                }
                objects.Clear();

                j++;
                if (j > 5)
                {
                    i++;
                    j = 1;
                }
                building = true;
            }
        }
        catch (System.Exception)
        {

        }
    }
}
