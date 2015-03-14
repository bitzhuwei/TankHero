using UnityEngine;
using System.Collections;

public class ThreeDSAnimation : MonoBehaviour
{

    public bool isPlaying;
    public bool loop = true;
    public float cycle = 0.4f;
    private float lastCycle = 0.4f;
    private System.Collections.Generic.List<GameObject> animationClips;
    private float interval;
    private float passedInterval;
    private int current;
    //private BulletEmitterBase bulletEmitter;
    //private HeadGun headGunScript;

    // Use this for initialization
    void Start()
    {
        //this.headGunScript = this.transform.parent.GetComponent<HeadGun>();
        animationClips = new System.Collections.Generic.List<GameObject>();
        var count = this.transform.childCount;
        current = 0;
        for (int i = 0; i < count; i++)
        {
            var child = this.transform.GetChild(i);
            var gameObj = child.gameObject;
            animationClips.Add(gameObj);
        }
        //this.bulletEmitter = this.transform.parent.GetComponent<BulletEmitterBase>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.bulletEmitter != null)
        //{ this.isPlaying = this.bulletEmitter.emitting; }

        if (!this.isPlaying) { return; }
        if (Time.deltaTime == 0) { return; }

        if (this.cycle != this.lastCycle)
        {
            this.interval = this.cycle / this.transform.childCount;
            this.lastCycle = this.cycle;
            if (this.interval < 0.01f)
            { this.interval = 0.01f; }
        }

        if (loop)
        {
            if (current >= animationClips.Count)
            { current = 0; }
            passedInterval += Time.deltaTime;
            if (passedInterval >= interval)
            {
                animationClips[current].SetActive(false);
                current++;
                if (current >= animationClips.Count)
                { current = 0; }
                animationClips[current].SetActive(true);

                passedInterval = 0;
            }
        }
        else
        {
            if (current >= animationClips.Count)
            {
                this.isPlaying = false;
                return;
            }

            passedInterval += Time.deltaTime;
            if (passedInterval >= interval)
            {
                current++;
                if (current < animationClips.Count)
                {
                    animationClips[current - 1].SetActive(false);
                    animationClips[current].SetActive(true);
                }
                passedInterval = 0;
            }
        }
    }
}