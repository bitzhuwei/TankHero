using UnityEngine;
using System.Collections;

public class ShockGunBullet : BulletBase
{

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
    public override void Destroy(MonoBehaviour other)
    {
        if(id%5==0)
        {
            base.Destroy(other);
        }
        else
        {
            //this.trans.rotation=
            this.rigidbody.AddForce(this.trans.rotation * Vector3.forward * speed * 3000, ForceMode.Force);
            id = 0;
        }
        
        base.Destroy(other);
    }
}
