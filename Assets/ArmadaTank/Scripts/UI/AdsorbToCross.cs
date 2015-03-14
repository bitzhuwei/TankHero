using UnityEngine;
using System.Collections;

public class AdsorbToCross : MonoBehaviour
{
    public bool putDown { get; set; }
    private float putDownInterval = 0.01f;
    private float passedPutDownInterval = 0;
    private float rotateInterval = 0.2f;
    private float passedRotateInterval = 0;
    private float moveUpDownInterval = 0.5f;
    private float passedMoveUpDownInterval;

    private float lastLeftClickTime;
    private float doubleClickInterval = 0.4f;
    private float lastRightClickTime;
    //private static readonly object synObj = new object();
    //private static int idCounter = 0;
    //private int id;
    //private static RaycastHit[] hits;

    //private GameObject adsorbObject;
    //private Camera thisCamera;

    void Awake()
    {
        //if (idCounter == 0)
        //{
        //    lock (synObj)
        //    {
        //        this.id = idCounter;
        //        idCounter++;
        //    }
        //}
        //else
        //{
        //    this.id = idCounter;
        //    idCounter++;
        //}
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera == null) { return; }

        //if (this.id == 0)
        //{
        //    Ray ray = thisCamera.ScreenPointToRay(Input.mousePosition);
        //    hits = Physics.RaycastAll(ray);
        //}
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        if (hits == null) { return; }

        if (putDown)
        {
            passedPutDownInterval += Time.deltaTime;
            if (passedPutDownInterval >= putDownInterval)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (Time.time - lastLeftClickTime < doubleClickInterval)
                    {
                        PickUp(hits);
                    }
                    lastLeftClickTime = Time.time;
                }
                else if(Input.GetMouseButtonUp(1))
                {
                    if(Time.time-lastRightClickTime<doubleClickInterval)
                    {
                        CopyThis(hits);
                    }
                    lastRightClickTime = Time.time;
                }
                passedPutDownInterval = 0;
                //if (Input.GetMouseButton(0))
                //{
                //    PickUp(hits);
                //    passedPutDownInterval = 0;
                //}
            }

        }
        else
        {
            AdsorbMove(hits);

            DestroyThis(hits);

            Rotate90();

            PutDown();

        }
    }

    private void CopyThis(RaycastHit[] hits)
    {
        foreach (var item in hits)
        {
            if (item.collider.tag == Tags.Plane) { continue; }
            if (item.collider.transform.parent == null) { continue; }
            if (item.collider.transform.parent.parent == null) { continue; }
            var adsorbObject = item.collider.transform.parent.parent.GetComponent<AdsorbToCross>();
            if (adsorbObject == this)
            {
                var camera = this.mainCamera;
                this.mainCamera = null;
                var newObj = Instantiate(this.gameObject) as GameObject;
                var script = newObj.GetComponent<AdsorbToCross>();
                script.putDown = false;
                script.mainCamera = camera;
                this.mainCamera = camera;
            }
            break;
        }

    }

    private void PutDown()
    {
        passedPutDownInterval += Time.deltaTime;
        if (passedPutDownInterval >= putDownInterval)
        {
            if (Input.GetMouseButton(0))
            {
                putDown = true;
                passedPutDownInterval = 0;
            }
        }
    }

    private void Rotate90()
    {
        passedRotateInterval += Time.deltaTime;
        if (passedRotateInterval >= rotateInterval)
        {
            if (Input.GetMouseButton(1))
            {
                this.transform.Rotate(0, 90, 0, Space.Self);
                passedRotateInterval = 0;
            }
        }
    }

    private void AdsorbMove(RaycastHit[] hits)
    {
        //var builder = new System.Text.StringBuilder("hits:");
        foreach (var item in hits)
        {
            //builder.AppendFormat("({0})", item.collider.name);
            var tag = item.collider.tag;
            if (tag == Tags.Plane)
            {
                var point = item.point;
                point.x = Mathf.RoundToInt(point.x);
                point.y = this.transform.position.y;// Mathf.RoundToInt(point.y);
                point.z = Mathf.RoundToInt(point.z);
                this.transform.position = point;
                break;
            }
        }
        passedMoveUpDownInterval += Time.deltaTime;
        if (passedMoveUpDownInterval >= moveUpDownInterval)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                var point = this.transform.position;
                point.y += 1;
                this.transform.position = point;
                passedMoveUpDownInterval = 0;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                var point = this.transform.position;
                point.y -= 1;
                this.transform.position = point;
                passedMoveUpDownInterval = 0;
            }
        }
        //Debug.Log(builder.ToString());
    }
    private void DestroyThis(RaycastHit[] hits)
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void PickUp(RaycastHit[] hits)
    {
        foreach (var item in hits)
        {
            if (item.collider.tag == Tags.Plane) { continue; }
            if (item.collider.transform.parent == null) { continue; }
            if (item.collider.transform.parent.parent == null) { continue; }
            var adsorbObject = item.collider.transform.parent.parent.GetComponent<AdsorbToCross>();
            if (adsorbObject == this)
            {
                adsorbObject.putDown = false;
            }
            break;
        }
    }


    public Camera mainCamera { get; set; }

}
