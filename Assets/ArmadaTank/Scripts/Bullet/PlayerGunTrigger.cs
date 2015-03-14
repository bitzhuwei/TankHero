using UnityEngine;
using System.Collections;

public class PlayerGunTrigger : MonoBehaviour
{
    private BulletEmitterBase bulletEmitter;
    private BattleFieldStateManager stateManager;
    void Awake()
    {
        this.bulletEmitter = this.GetComponent<BulletEmitterBase>();
    }
    // Use this for initialization
    void Start()
    {
        var obj = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
        this.stateManager = obj.GetComponent<BattleFieldStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.stateManager)
        {
            if (this.stateManager.state == EBattleFieldState.GoodGame
              || this.stateManager.state == EBattleFieldState.Paused
              || this.stateManager.state == EBattleFieldState.Win)
            {
                this.bulletEmitter.emitting = false;
                return;
            }
        }
        if (Input.GetMouseButton(0))
        {
            this.bulletEmitter.emitting = true;
        }
        else
        {
            this.bulletEmitter.emitting = false;
        }
    }
}
