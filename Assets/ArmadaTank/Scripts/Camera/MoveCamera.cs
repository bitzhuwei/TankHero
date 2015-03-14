using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{

    public float speed = 8;
    public float ScrollSpeed = 300;
    private Transform cameraTransform;
    // Use this for initialization
    void Start()
    {
        this.cameraTransform = this.camera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime <= 0) { return; }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        this.cameraTransform.position += new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);

        var wheel = Input.GetAxis("Mouse ScrollWheel");
        this.cameraTransform.position += new Vector3(0, -wheel * ScrollSpeed * Time.deltaTime, 0);

        if (this.cameraTransform.position.y <= 2f)
        {
            var position = this.cameraTransform.position;
            position.y = 2f;
            this.cameraTransform.position = position;
        }
    }
}
