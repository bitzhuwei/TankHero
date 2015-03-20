using UnityEngine;
using System.Collections;

public class PlayerHealth : TankHealth {
    public BaseModel baseModelScript;

    protected override IConfig GetBaseModelConfig()
    {
        return baseModelScript;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (this.health <= 0) { return; }

        var bulletScript = other.GetComponentInParent<BulletBase>();
        if (bulletScript == null) { return; }
        if (!(bulletScript.emitter is PlayerBulletEmitter))
        {
            this.health -= bulletScript.damage;
        }
        bulletScript.Destroy(this); 
    }

    protected override void OnDead()
    {
    }
}
