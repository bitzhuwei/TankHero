using UnityEngine;
using System.Collections;

public class MainMenuPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MainMenuPlay_Click()
    {
        Application.LoadLevel(Scenes.SelectMap);
    }
}
