using UnityEngine;
using System.Collections;

public class btnFarCamera : MonoBehaviour {

    public TestWallInfo testWallInfo;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btnFarCamera_Click()
    {
        Camera.main.transform.position = new Vector3(0, 0, -7);
        Camera.main.transform.eulerAngles = new Vector3(13.5f, 0, 0);
    }
}
