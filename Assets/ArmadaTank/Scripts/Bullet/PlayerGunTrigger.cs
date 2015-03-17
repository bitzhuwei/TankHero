using UnityEngine;
using System.Collections;

public class PlayerGunTrigger : MonoBehaviour
{
    private BulletEmitterBase bulletEmitter;
    private BattleFieldStateManager stateManager;
    private AndroidTouchState touchState;
    void Awake()
    {
        this.bulletEmitter = this.GetComponent<BulletEmitterBase>();
    }
    // Use this for initialization
    void Start()
    {
        this.touchState = this.GetComponentInParent<AndroidTouchState>();
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
#if UNITY_ANDROID
        var fireTarget = this.touchState.GetPlayerFireTarget();
        this.bulletEmitter.emitting = fireTarget.HasValue;
#else
        this.bulletEmitter.emitting = Input.GetMouseButton(0);
#endif
    }
}
