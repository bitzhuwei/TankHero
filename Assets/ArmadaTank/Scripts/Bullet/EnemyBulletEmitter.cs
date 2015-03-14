using UnityEngine;
using System.Collections;

public class EnemyBulletEmitter : BulletEmitterBase {

    //private HeadGun gun;
    private EnemyModel enemyModelScript;
    private EnemyModel.PrefabOption lastEnemyModelPrefab;
    private TankBaseRotation tankBaseRotationScript;
    //private ProjectileSpeed projectileSpeed;
    //private HeadGun.PrefabOption prefab;

    
    protected override void Awake()
    {
        base.Awake();
        //this.gun = this.GetComponent<HeadGun>();
        this.enemyModelScript = this.GetComponent<EnemyModel>();
        this.tankBaseRotationScript = this.GetComponent<TankBaseRotation>();
        //this.projectileSpeed = this.GetComponent<ProjectileSpeed>();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        RefreshEnemyConfig();
    }

    private void RefreshEnemyConfig()
    {
        //this.reloadTime = this.gun.GetReloadTime();
        this.reloadTime = this.enemyModelScript.GetReloadTime();
        this.passedReloadTime = this.reloadTime;
        //this.bulletName = this.gun.GetBulletName();
        this.bulletName = this.enemyModelScript.GetBulletName();
        //var name = this.gun.GetAudioClipName();
        var name = this.enemyModelScript.GetAudioClipName();
        if (audioClipManager)
        { this.emittingSound.clip = audioClipManager.AudioClipDict[name]; }
        //this.prefab = this.gun.prefab;
        this.damage = this.enemyModelScript.GetDamage();
        this.projectileLevel = this.enemyModelScript.GetProjectileLevel();
        this.foundShootingAnimation = false;
        this.lastEnemyModelPrefab = this.enemyModelScript.prefab;
    }

    // Update is called once per frame
    protected override void Update()
    {
        passedReloadTime += Time.deltaTime;

        if (!this.emitting) { return; }
        if (!this.tankBaseRotationScript.IsRotationCompleted) { return; }
        if (passedReloadTime < this.reloadTime) { return; }

        if (this.lastEnemyModelPrefab != this.enemyModelScript.prefab)
        { RefreshEnemyConfig(); }

        EmitBullet();
        passedReloadTime = 0;
        emittingSound.Play();
    }

    protected override void EmitBullet()
    {
        if (bulletName == PrefabName.strbullet_ShockGun)
        {
            for (int i = 0; i < 5; i++)
            {
                base.EmitBullet();
            }
        }
        else
        {
            base.EmitBullet();
        }
    }
}
