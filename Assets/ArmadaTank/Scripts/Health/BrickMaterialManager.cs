using UnityEngine;
using System.Collections;

public class BrickMaterialManager {
    private static System.Collections.Generic.Dictionary<string, string> seriesDict;
    private static System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<Material>> nextMaterial;

    public BrickMaterialManager()
    {
        if (nextMaterial == null)
        {
            var brickMatNames = new string[][] {
                new string[]{
                    MaterialName.strTerrWall_C_0,
                    MaterialName.strTerrWall_C_1,
                    MaterialName.strTerrWall_C_2,
                    MaterialName.strTerrWall_C_3,
                },
                new string[]{
                    MaterialName.strTerrWall_G_0,
                },
                new string[]{
                    MaterialName.strTerrWall_R_0,
                    MaterialName.strTerrWall_R_1,
                    MaterialName.strTerrWall_R_2,
                    MaterialName.strTerrWall_R_3,
                },
                new string[]{
                    MaterialName.strTerrWall_W_0,
                    MaterialName.strTerrWall_W_1,
                    MaterialName.strTerrWall_W_2,
                    MaterialName.strTerrWall_W_3,
                              },
                new string[]{
                    MaterialName.strTerrWall_Y_0,
                    MaterialName.strTerrWall_Y_1,
                    MaterialName.strTerrWall_Y_2,
                    MaterialName.strTerrWall_Y_3,
                },
                new string[]{
                    MaterialName.strTerr_01,
                }
            };
            seriesDict = new System.Collections.Generic.Dictionary<string, string>();
            foreach (var item in brickMatNames)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    seriesDict.Add(item[i], item[0]);
                }
            }
            nextMaterial = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<Material>>();
            foreach (var item in brickMatNames)
            {
                var list = new System.Collections.Generic.List<Material>();
                for (int i = item.Length - 1; i >=0;i--)
                {
                    list.Add(ResourcesManager.GetMaterial(item[i]));
                }
                nextMaterial.Add(item[0], list);
            }
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Material GetMaterial(string seriesKey, int indexByHealth)
    {
        var series = seriesDict[seriesKey];//.mainTexture];
        var list = nextMaterial[series];
        if (indexByHealth < 0) { indexByHealth = 0; }
        if (indexByHealth >= list.Count) { indexByHealth = list.Count - 1; }

        return list[indexByHealth];
    }
}
