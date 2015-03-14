using UnityEngine;
using System.Collections;

public class GenerateMaterialsName : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var assets = Resources.LoadAll("Materials");
        var builder = new System.Text.StringBuilder();
        foreach (var item in assets)
        {
            builder.AppendLine(string.Format("public const string str{0} = {1}{0}{1};",
                item.name, "\""));
        }
        var result = builder.ToString();
        Debug.Log(result);
        //var x = ResourcesManager.GetMaterial("x");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
