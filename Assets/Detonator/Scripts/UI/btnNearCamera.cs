using UnityEngine;
using System.Collections;

public class btnNearCamera : MonoBehaviour {

    public TestWallInfo testWallInfo;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btnNearCamera_Click()
    {
        Camera.main.transform.position = new Vector3(0, -8.664466f, 31.38269f);
        Camera.main.transform.eulerAngles = new Vector3(1.213462f, 0, 0);
    }
}
