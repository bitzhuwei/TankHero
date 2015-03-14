using UnityEngine;
using System.Collections;

public class RotateY : MonoBehaviour
{

    public float rotationSpeed = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, Mathf.Rad2Deg * rotationSpeed * Time.deltaTime, 0, Space.Self);
    }
}
