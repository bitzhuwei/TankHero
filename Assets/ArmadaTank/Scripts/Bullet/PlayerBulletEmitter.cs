using UnityEngine;
using System.Collections;

public class PlayerBulletEmitter : BulletEmitterBase {

    private HeadGun gun;
    private ProjectileSpeed projectileSpeed;
    private HeadGun.PrefabOption lastGunPrefab;
    protected override void Awake()
    {
        base.Awake();
        this.gun = this.GetComponent<HeadGun>();
        this.gun.damageChanged += gun_damageChanged;
        this.gun.reloadTimeChanged += gun_reloadTimeChanged;
    }

    void projectileSpeed_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
    {
        this.projectileLevel = this.projectileSpeed.value;
    }

    void gun_reloadTimeChanged(float obj)
    {
        this.reloadTime = obj;
    }

    void gun_damageChanged(float obj)
    {
        this.damage = obj;
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        this.projectileSpeed = this.transform.parent.GetComponentInChildren<ProjectileSpeed>();
        this.projectileSpeed.prefabChanged += projectileSpeed_prefabChanged;
        RefreshPlayerConfig();
    }

    private void RefreshPlayerConfig()
    {
        this.reloadTime = this.gun.reloadTime;
        this.passedReloadTime = this.reloadTime;
        this.bulletName = this.gun.GetBulletName();
        var name = this.gun.GetAudioClipName();
        if (audioClipManager)
        { this.emittingSound.clip = audioClipManager.AudioClipDict[name]; }
        this.foundShootingAnimation = false;
        this.lastGunPrefab = this.gun.prefab;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //Debug.Log(string.Format("shootingAnimation: {0}", this.shootingAnimation));
        passedReloadTime += Time.deltaTime;
        if (!this.emitting) { return; }
        if (passedReloadTime < this.reloadTime) { return; }

        if (this.lastGunPrefab != this.gun.prefab)
        {
            RefreshPlayerConfig();
        }

       
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
