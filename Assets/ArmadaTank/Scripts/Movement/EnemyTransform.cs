using UnityEngine;
using System.Collections;

public class EnemyTransform : TankTransform
{
    //private static TankToward[] tankTowards = new TankToward[] { TankToward.Z, TankToward.NZ, TankToward.NX, TankToward.X, };
    public override TankToward GetNextMovementToward()
    {
        var possibleTowards = this.tankTranslateScript.GetUnBlokedTowards();
        if (possibleTowards.Count == 0) { return TankToward.None; }

        var index = Random.Range(0, possibleTowards.Count);
        return possibleTowards[index];
    }
}
