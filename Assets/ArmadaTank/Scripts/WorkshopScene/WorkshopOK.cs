using UnityEngine;
using System.Collections;

public class WorkshopOK : MonoBehaviour {

    public WorkshopConfigLoader configLoader;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OK_Click()
    {
        configLoader.config.Save(ConfigFilenames.workshopConfig);
        Application.LoadLevel(Scenes.BattleField);
    }
}
