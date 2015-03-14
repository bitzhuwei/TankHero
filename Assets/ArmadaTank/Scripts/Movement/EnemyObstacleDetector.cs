using UnityEngine;
using System.Collections;

public class EnemyObstacleDetector : ObstacleDetector {

    protected override void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        if (tag != null)
        {
            if (tag == Tags.EnemyObstacleDetector) { return; }
            if (tag == Tags.PlayerObstacleDetector) { return; }
            if (tag == Tags.EnemyObstacleDetectorCenter) { return; }
        }

        //Debug.LogError(string.Format("enter: {0}", other.gameObject.name));
        this.obstacles.Add(other.gameObject);
    }
}
