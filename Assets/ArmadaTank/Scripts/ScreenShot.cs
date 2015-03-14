using UnityEngine;
using System.Collections;

public class ScreenShot : MonoBehaviour
{

    private int count;
    private int current;
    // Use this for initialization
    void Start()
    {

        count = this.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (current == count) { return; }
        if (current != 0)
        { this.transform.GetChild(current - 1).gameObject.SetActive(false); }
        var child = this.transform.GetChild(current++);
        child.gameObject.SetActive(true);
        Application.CaptureScreenshot("PrefabShot/" + child.gameObject.name+".jpg");
    }
}
