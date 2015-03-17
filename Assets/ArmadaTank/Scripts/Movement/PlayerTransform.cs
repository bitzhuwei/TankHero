using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerTransform : TankTransform
{
    private BattleFieldStateManager stateManager;
    private AndroidTouchState touchState;

    public override void Start()
    {
        this.touchState = this.GetComponent<AndroidTouchState>();
        var obj = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
        if (obj)
        { this.stateManager = obj.GetComponent<BattleFieldStateManager>(); }

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

#if UNITY_ANDROID
        result = touchState.GetPlayerTankToward();
#else
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (Mathf.Abs(h) > Quaternion.kEpsilon || Mathf.Abs(v) > Quaternion.kEpsilon)
        {
            if (Mathf.Abs(h) >= Mathf.Abs(v))
            {
                result = h > 0 ? TankToward.X : TankToward.NX;
            }
            else
            {
                result = v > 0 ? TankToward.Z : TankToward.NZ;
            }
        }
#endif

        return result;
    }

}
