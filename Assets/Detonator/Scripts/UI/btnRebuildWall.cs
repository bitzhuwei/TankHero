using UnityEngine;
using System.Collections;

public class btnRebuildWall : MonoBehaviour {

    public TestWallInfo testWallInfo;
    // Use this for initialization
    void Start()
    {
        btnRebuildWall_Click();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btnRebuildWall_Click()
    {
        testWallInfo.SpawnWall();
    }
}
