using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour {

    private GameObject SelectedObjectManager;
    private SelectedObjectManager manager;
    private UnityEngine.UI.Text buttonText;

	// Use this for initialization
	void Start () {
        this.SelectedObjectManager = GameObject.FindGameObjectWithTag(Tags.SelectedObjectManager);
        this.manager = this.SelectedObjectManager.GetComponent<SelectedObjectManager>();
        this.buttonText = this.GetComponentInChildren<UnityEngine.UI.Text>();
        this.buttonText.text = string.Format("Selecting: {0}", this.manager.GetCurrent().name); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectObject_Click()
    {
        var current = this.manager.SelectNext();
        this.buttonText.text = string.Format("Selecting: {0}", current.name); 
    }
}
