using UnityEngine;
using System.Collections;

public class ClearMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ClearMap_Click()
    {
        var objs = GameObject.FindObjectsOfType<GameObject>();
        foreach (var item in objs)
        {
            var serializeScript = item.GetComponent<Serialize>();
            if (serializeScript == null) { continue; }

            Destroy(item);
        }
    }
}
