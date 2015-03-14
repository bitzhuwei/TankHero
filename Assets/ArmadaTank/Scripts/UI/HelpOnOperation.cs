using UnityEngine;
using System.Collections;

public class HelpOnOperation : MonoBehaviour {

    public UnityEngine.UI.Text txtHelpInfo;
    public float displayInterval = 10f;
    private float passedDisplayInterval;
    private bool displaying;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (displaying)
        {
            passedDisplayInterval += Time.deltaTime;
            if (passedDisplayInterval >= displayInterval)
            {
                this.txtHelpInfo.enabled = false;
                displaying = false;
                passedDisplayInterval = 0;
            }
        }
	}

    public void Help_Click()
    {
        this.displaying = !this.displaying;
        this.txtHelpInfo.enabled = this.displaying;
    }
}
