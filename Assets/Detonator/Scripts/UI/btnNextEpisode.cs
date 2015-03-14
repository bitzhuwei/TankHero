using UnityEngine;
using System.Collections;

public class btnNextEpisode : MonoBehaviour {

    public TestWallInfo testWallInfo;
    private UnityEngine.UI.Text buttonText;
	// Use this for initialization
	void Start () {
        this.buttonText = this.GetComponentInChildren<UnityEngine.UI.Text>();
        btnNextEpisode_Click();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void btnNextEpisode_Click()
    {
        testWallInfo.NextEpisode();
        this.buttonText.text = string.Format("{0} (Click For Next)",
            testWallInfo.currentDetonator.name);
    }
}
