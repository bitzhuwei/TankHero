using UnityEngine;
using System.Collections;

public class GetObject : MonoBehaviour
{

    public Camera mainCamera;
    private SelectedObjectManager manager;
    public AdsorbToCross currentObject { get; set; }
    // Use this for initialization
    void Start()
    {
        var obj = GameObject.FindGameObjectWithTag(Tags.SelectedObjectManager);
        this.manager = obj.GetComponent<SelectedObjectManager>();
    }


    public void GetObject_Click()
    {
        var obj = this.currentObject;
        if (obj != null && !obj.putDown)
        {
            DestroyCurrentObject();
        }
        var current = this.manager.GetCurrent();
        var newObj = Instantiate(current) as GameObject;
        newObj.AddComponent<Serialize>();
        var script = newObj.AddComponent<AdsorbToCross>();
        script.mainCamera = this.mainCamera;
        this.currentObject = script;
    }


    private void DestroyCurrentObject()
    {
        var obj = this.currentObject;
        if (obj != null)
        {
            this.currentObject = null;
            Destroy(obj.gameObject);
        }
    }

    void Update()
    {
        //var obj = this.currentObject;
        //if (obj == null) { return; }

        //if (obj.putDown)
        //{ this.currentObject = null; }
        //else
        //{
        //    if (Input.GetKey(KeyCode.Escape))
        //    {
        //        DestroyCurrentObject();
        //    }
        //}
    }
}
