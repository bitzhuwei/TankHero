using UnityEngine;
using System.Collections;

public class FirethrowerBullet : BulletBase
{
    private static readonly float basicSpeed = 0.03f;
    private static readonly float[] FirethrowerSpeed = new float[] { 0.02f, 0.03f, 0.04f, 0.05f, 0.06f };
    public override void Set(int speedLevel, float damage, BulletEmitterBase emitter)
    {
        this.speed = basicSpeed + FirethrowerSpeed[speedLevel];
        this.damage = damage;
        this.emitter = emitter;
        Destroy(this.gameObject, 1.5f);
    }


    //// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}
}
