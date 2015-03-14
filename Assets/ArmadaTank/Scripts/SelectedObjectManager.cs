using UnityEngine;
using System.Collections;

public class SelectedObjectManager : MonoBehaviour {

    private System.Collections.Generic.List<GameObject> targetObjects;
    private int current;

	// Use this for initialization
	void Start () {
        this.targetObjects = new System.Collections.Generic.List<GameObject>();
        var count = this.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            this.targetObjects.Add(this.transform.GetChild(i).gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject SelectNext()
    {
        this.targetObjects[current].SetActive(false);
        current++;
        if (current >= this.targetObjects.Count)
        { current = 0; }
        this.targetObjects[current].SetActive(true);
        return this.targetObjects[current];
    }

    public GameObject GetCurrent()
    {
        if (this.targetObjects == null
            || this.targetObjects.Count <= current) { return null; }
        return this.targetObjects[current];
    }
}
