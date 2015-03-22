using UnityEngine;
using System.Collections;

public class UIGetMoneyFactory : MonoBehaviour {

    public RectTransform UIGetMoneyPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Create(Transform transform)
    {
        var obj = Instantiate(UIGetMoneyPrefab) as RectTransform;
        var point = Camera.main.WorldToScreenPoint(transform.position);
        obj.position = point;
        obj.SetParent(this.transform);
        //obj.parent = this.transform;
    }
}
