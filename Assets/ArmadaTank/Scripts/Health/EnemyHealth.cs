using UnityEngine;
using System.Collections;

public class EnemyHealth : TankHealth
{
    public EnemyModel baseModelScript;
    private WinInBattleField winInBattleField;
    private UIGetMoneyFactory getMoneyFactory;

    protected override void Start()
    {
        base.Start();
        base.health = baseModelScript.GetHealth();
        {
            var battleFieldBuilder = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
            this.winInBattleField = battleFieldBuilder.GetComponent<WinInBattleField>();
        }
        {
            var canvas = GameObject.FindGameObjectWithTag(Tags.BattleFieldCanvas);
            this.getMoneyFactory = canvas.GetComponent<UIGetMoneyFactory>();
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
        this.getMoneyFactory.Create(this.transform);
        winInBattleField.gainedMoney += Random.Range(1,100);
    }
}
