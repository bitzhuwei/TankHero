using UnityEngine;
using System.Collections;

public class EnemyHealth : TankHealth
{
    public EnemyModel baseModelScript;

    protected override void Awake()
    {
        base.Awake();
        base.health = baseModelScript.GetHealth();
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
}
