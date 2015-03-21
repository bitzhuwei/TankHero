using UnityEngine;
using System.Collections;

public class OK_BattleField : MonoBehaviour {

    public WinInBattleField winInBattleField;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void WinPanelOK_Click()
    {
        {
            var obj = GameObject.FindGameObjectWithTag(Tags.SelectMapManager);
            var manager = obj.GetComponent<SelectMapManager>();
            var content = FileHelper.Read(ConfigFilenames.SelectMapConfig);
            var config = SelectMapConfig.Parse(content);
            var currentMap = string.Format("{0}{1}", manager.selectedEpisode, manager.selectedMap);
            if (!config.warProgressList.Contains(currentMap))
            {
                config.warProgressList.Add(currentMap);
                config.Save(ConfigFilenames.SelectMapConfig);
            }
            Destroy(obj);
        }
        {
            var content = FileHelper.Read(ConfigFilenames.workshopConfig);
            var config = WorkshopConfig.Parse(content);
            config.money += this.winInBattleField.gainedMoney;
            config.Save(ConfigFilenames.workshopConfig);
        }
        {
            var obj = GameObject.FindGameObjectWithTag(Tags.WorkshopConfig);
            Destroy(obj);
        }
        Application.LoadLevel(Scenes.SelectMap);
    }
}
