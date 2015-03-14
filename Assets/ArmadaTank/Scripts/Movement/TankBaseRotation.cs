using UnityEngine;
using System.Collections;

public class TankBaseRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;//degrees
    private TankTransform tankTransformScript;
    //private Vector3 lastMovingVector;
    private Quaternion startRotation;
    private float startTime;
    private Quaternion targetRotation;
    public bool IsRotationCompleted { get; protected set; }


    // Use this for initialization
    void Start()
    {
        this.tankTransformScript = this.GetComponentInParent<TankTransform>();
        this.IsRotationCompleted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (movementScript == null) { return; }

        if (IsRotationCompleted)
        {
            //if (tankTransformScript.targetMovementVector != Vector3.zero
                //&& tankTransformScript.targetMovementVector != lastMovingVector)
            if (tankTransformScript.targetMovementVector !=  TankToward.None
                && tankTransformScript.targetMovementVector != tankTransformScript.tankBaseToward)
            {
                //lastMovingVector = tankTransformScript.targetMovementVector;
                startTime = Time.time;
                startRotation = this.transform.rotation;
                targetRotation = Quaternion.LookRotation(TankTowardHepler.ToVector3(tankTransformScript.targetMovementVector), Vector3.up);
                IsRotationCompleted = false;
            }
        }

        if (!IsRotationCompleted)
        {
            var t = rotationSpeed * (Time.time - startTime);
            this.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            if (t > 1)
            {
                var tmp = tankTransformScript.targetMovementVector;
                tankTransformScript.targetMovementVector = TankToward.None;
                tankTransformScript.tankBaseToward = tmp;
                IsRotationCompleted = true;
            }
        }
    }
}
