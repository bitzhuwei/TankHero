using UnityEngine;
using System.Collections;

public class CanonBullet : BulletBase {

    public Transform explosionPrefab;
	// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    public override void Destroy(MonoBehaviour other)
    {
        base.Destroy(other);
        Instantiate(explosionPrefab, this.trans.position, this.trans.rotation);
    }
}
