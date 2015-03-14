using UnityEngine;
using System.Collections;

public class PlayerObstacleDetector : ObstacleDetector
{

    protected override void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        if (tag != null)
        {
            if (tag == Tags.EnemyObstacleDetector) { return; }
            if (tag == Tags.PlayerObstacleDetector) { return; }
        }

        this.obstacles.Add(other.gameObject);
    }

}
