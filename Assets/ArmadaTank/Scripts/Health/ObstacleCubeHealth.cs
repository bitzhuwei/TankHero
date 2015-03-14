using UnityEngine;
using System.Collections;

public class ObstacleCubeHealth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        var bulletScript = other.GetComponentInParent<BulletBase>();
        if (bulletScript != null)
        {
            bulletScript.Destroy(this);
        }


    }
}
