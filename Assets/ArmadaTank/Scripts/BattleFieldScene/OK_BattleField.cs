using UnityEngine;
using System.Collections;

public class OK_BattleField : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void WinPanelOK_Click()
    {
        var selectMapManager = GameObject.FindGameObjectWithTag(Tags.SelectMapManager);
        var manager = selectMapManager.GetComponent<SelectMapManager>();
        var content = FileHelper.Read(ConfigFilenames.SelectMapConfig);
        var parts = content.Split(new char[] { ';', '\r', '\t', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        var list = new System.Collections.Generic.List<string>(parts);
        list.Add(string.Format("{0}{1}", manager.selectedEpisode, manager.selectedMap));
        var builder = new System.Text.StringBuilder();
        foreach (var item in list)
        {
            builder.Append(item);
            builder.AppendLine(";");
        }
        var updatedConfig = builder.ToString();
        Debug.Log(string.Format("new config:{0}", updatedConfig));
        FileHelper.Write(ConfigFilenames.SelectMapConfig, updatedConfig); 
        Application.LoadLevel(Scenes.SelectMap);
    }
}
