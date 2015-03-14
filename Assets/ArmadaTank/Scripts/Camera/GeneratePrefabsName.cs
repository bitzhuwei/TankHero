using UnityEngine;
using System.Collections;

public class GeneratePrefabsName : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var assets = Resources.LoadAll("Prefabs/BattleField");
        var builder = new System.Text.StringBuilder();
        foreach (var item in assets)
        {
            builder.AppendLine(string.Format("public const string str{0} = {1}{0}{1};",
                item.name, "\""));
        }
        var result = builder.ToString();
        Debug.Log(result);
        builder = new System.Text.StringBuilder();
        foreach (var item in assets)
        {
            if (item.name.Contains("err_"))
            {
                builder.AppendLine(string.Format("terrDict.Add(\"{0}\", str{1});",
                    item.name.Substring("Terr_".Length), item.name));
            }
        }
        //for (int i = 0; i < 111; i++)
        //{
        //    builder.AppendLine(string.Format("Terr_{0},", i));
        //}
        Debug.Log(builder.ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
