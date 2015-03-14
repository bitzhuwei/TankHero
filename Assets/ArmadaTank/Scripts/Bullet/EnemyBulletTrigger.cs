using UnityEngine;
using System.Collections;

public class EnemyBulletTrigger : MonoBehaviour {

    private BulletEmitterBase bulletEmitter;

    void Awake()
    {
        this.bulletEmitter = this.GetComponent<BulletEmitterBase>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var value = Random.Range(0, 10);
        //Debug.Log(string.Format("Random.Range({0}, {1}) is {2}", 0, 10, value));
        // value is from 0 to 9. 10 is not included.
        if (value < 1)
        {
            this.bulletEmitter.emitting = true;
        }
        else
        {
            this.bulletEmitter.emitting = false;
        }
    }
}
