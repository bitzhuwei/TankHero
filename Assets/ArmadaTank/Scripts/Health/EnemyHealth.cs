using UnityEngine;
using System.Collections;

public class EnemyHealth : TankHealth
{
    public EnemyModel baseModelScript;
    public WinInBattleField winInBattleField;

    protected override void Awake()
    {
        base.Awake();
        base.health = baseModelScript.GetHealth();
        {
            var battleFieldBuilder = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
            this.winInBattleField = battleFieldBuilder.GetComponent<WinInBattleField>();
        }
    }
    protected override IConfig GetBaseModelConfig()
    {
        return baseModelScript;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (this.health <= 0) { return; }

        var bulletScript = other.GetComponentInParent<BulletBase>();
        if (bulletScript == null) { return; }
        if (!(bulletScript.emitter is EnemyBulletEmitter))
        {
            this.health -= bulletScript.damage;
        }
        bulletScript.Destroy(this);
    }

    protected override void OnDead()
    {
        winInBattleField.gainedMoney += Random.Range(1,100);
    }
}
