using UnityEngine;
using System.Collections;

public class PlaneUpDown : MonoBehaviour
{

    public int maxHeight = 4;
    public int minHeight = -1;
    public float interval = 0.2f;
    private float passedInterval;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        passedInterval += Time.deltaTime;
        if (passedInterval >= interval)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                var position = this.transform.position;

                if (position.y >= maxHeight)
                {
                    position.y = minHeight;
                }
                else
                {
                    position.y += 1;
                }
                this.transform.position = position;
                passedInterval = 0;
            }
        }

    }
}
