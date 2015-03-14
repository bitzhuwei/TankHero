using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {
    public WorkshopConfigLoader configLoader;
    private UnityEngine.UI.Text text;

    void Awake()
    {
        this.text = this.GetComponent<UnityEngine.UI.Text>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.text.text = string.Format("Money: {0}",
            configLoader.config.money);
	}
}
