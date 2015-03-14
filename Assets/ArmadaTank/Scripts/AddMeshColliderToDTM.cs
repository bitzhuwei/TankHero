using UnityEngine;
using System.Collections;

public class AddMeshColliderToDTM : MonoBehaviour {

    public bool convex;
    public bool isTrigger;
	// Use this for initialization
	void Start () {
        var count = this.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            var child = this.transform.GetChild(i);
            if (child.gameObject.GetComponent<MeshCollider>() == null)
            {
                var collider = child.gameObject.AddComponent<MeshCollider>();
                collider.convex = convex;
                collider.isTrigger = isTrigger;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
