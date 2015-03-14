using UnityEngine;
using System.Collections;

public class PlayerTransform : TankTransform
{
    private BattleFieldStateManager stateManager;

    public override void Start()
    {
        var obj = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
        this.stateManager = obj.GetComponent<BattleFieldStateManager>();
        base.Start();
    }
    public override TankToward GetNextMovementToward()
    {
        var result = TankToward.None;
        if (this.stateManager)
        {
            if (this.stateManager.state == EBattleFieldState.GoodGame
              || this.stateManager.state == EBattleFieldState.Paused
              || this.stateManager.state == EBattleFieldState.Win)
            { return result; }
        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > Quaternion.kEpsilon || Mathf.Abs(v) > Quaternion.kEpsilon)
        {
            if (Mathf.Abs(h) >= Mathf.Abs(v))
            {
                result = h > 0 ? TankToward.X: TankToward.NX;
            }
            else
            {
                result = v > 0 ? TankToward.Z:TankToward.NZ;
            }
        }
        return result;
    }
}
