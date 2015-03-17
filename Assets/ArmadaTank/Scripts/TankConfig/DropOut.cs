using UnityEngine;
using System.Collections;

public class DropOut : MonoBehaviour
{
    public float dropOutSpeed = 1f;
    private Vector3 destPosition;
    public ThreeDSAnimation threeDSAnimation;
    private float passed;
    private float startFadeOutTime;
    private Vector3 startPosition;
    // Use this for initialization
    void Start()
    {
        startFadeOutTime = 0;
        //this.threeDSAnimation = this.GetComponent<ThreeDSAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFadeOutTime > 0)
        {
            var t = (Time.time - startFadeOutTime) * dropOutSpeed;
            this.transform.position = Vector3.Lerp(startPosition, destPosition, t);
            if (t > 1)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            passed += Time.deltaTime;
            if (passed >= threeDSAnimation.cycle * 3)
            {
                startFadeOutTime = Time.time;
                startPosition = this.transform.position;
                destPosition = startPosition;
                destPosition.y -= 1;
            }
        }
    }
}
