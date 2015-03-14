using UnityEngine;
using System.Collections;

public class Brick4Health : MonoBehaviour {

    private BrickHealth[] brickHealthArray;
    private bool allDead;

    void Awake()
    {
        this.allDead = false;
    }
	// Use this for initialization
	void Start () {
        this.brickHealthArray = this.GetComponentsInChildren<BrickHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if (allDead) { return; }
        allDead = GetAllDead();
        if(allDead)
        {
            for (int i = 0; i < this.brickHealthArray.Length; i++)
            {
                this.brickHealthArray[i].AllDead();
            }
            Destroy(this.gameObject);
        }
	}

    private bool GetAllDead()
    {
        var result = true;
        for (int i = 0; i < this.brickHealthArray.Length; i++)
        {
            if (this.brickHealthArray[i].health > 0)
            {
                result = false;
                break;
            }
        }
        return result;
    }
}
