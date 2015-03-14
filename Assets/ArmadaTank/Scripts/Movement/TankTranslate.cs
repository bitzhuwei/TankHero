using UnityEngine;
using System.Collections;

public class TankTranslate : MonoBehaviour
{
    private System.Collections.Generic.Dictionary<TankToward, ObstacleDetector> obstacleDetectorDict;
    private TankTransform tankTransform;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float startTime;
    public bool IsMovementCompleted { get; protected set; }

    // Use this for initialization
    void Start()
    {
        this.tankTransform = this.GetComponent<TankTransform>();
        this.IsMovementCompleted = true;
        if (obstacleDetectorDict == null)
        { obstacleDetectorDict = new System.Collections.Generic.Dictionary<TankToward, ObstacleDetector>(); }
        var names = new string[] { "forward", "backward", "left", "right" };
        var direction = new TankToward[] { TankToward.Z, TankToward.NZ, TankToward.NX, TankToward.X };
        //var direction = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        for (int i = 0; i < names.Length; i++)
        {
            var child = this.transform.FindChild(names[i]);
            var script = child.GetComponent<ObstacleDetector>();
            obstacleDetectorDict.Add(direction[i], script);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMovementCompleted)
        {
            if (tankTransform.targetMovementVector == tankTransform.tankBaseToward)
            {
                var obstacleDetector = obstacleDetectorDict[tankTransform.targetMovementVector];
                if (obstacleDetector.IsUnblocked())
                {
                    this.startTime = Time.time;
                    this.startPosition = this.transform.position;
                    this.targetPosition = this.startPosition + TankTowardHepler.ToVector3(tankTransform.targetMovementVector);
                    this.IsMovementCompleted = false;
                }
            }
        }

        if (!IsMovementCompleted)
        {
            var t = 2 * tankTransform.speed * (Time.time - startTime);
            this.transform.position = Vector3.Lerp(this.startPosition, this.targetPosition, t);
            if (t > 1)
            {
                this.tankTransform.targetMovementVector = TankToward.None;
                this.IsMovementCompleted = true;
            }
        }
    }



    public System.Collections.Generic.List<TankToward> GetUnBlokedTowards()
    {
        var result = new System.Collections.Generic.List<TankToward>();
        var list = new TankToward[] { TankToward.Z, TankToward.NZ, TankToward.NX, TankToward.X, };
        foreach (var item in list)
        {
            var blocks = false;
            var direction = this.obstacleDetectorDict[item];
            foreach (var obstacle in direction.obstacles)
            {
                if (obstacle && obstacle.name == "ObstacleCube")
                {
                    blocks = true;
                    break;
                }
            }
            if (!blocks)
            {
                result.Add(item);
            }
        }
        return result;
    }
}
