using UnityEngine;
using System.Collections;

public class FlashRotate : MonoBehaviour {

    public float rotationSpeed = 1;
    private Transform trans;
    // Use this for initialization
    void Start()
    {
        this.trans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.trans.Rotate(0, 0, Mathf.Rad2Deg * rotationSpeed * Time.deltaTime);
    }
}
