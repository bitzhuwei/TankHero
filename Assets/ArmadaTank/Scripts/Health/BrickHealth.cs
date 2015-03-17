using UnityEngine;
using System.Collections;

public class BrickHealth : MonoBehaviour
{
    //static System.Collections.Generic.Dictionary<Material, Material> nextMaterial;
    static BrickMaterialManager manager;
    public float health;
    private GameObject deadAnimation;
    private string initialMaterial;
    void Awake()
    {
        this.health = 99;
        if (manager == null) { manager = new BrickMaterialManager(); }
    }

    // Use this for initialization
    void Start()
    {
        this.initialMaterial = this.renderer.material.name.Substring(0, "TerrWall_G_0".Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && this.deadAnimation == null)
        {
            this.renderer.enabled = false;
            deadAnimation = ResourcesManager.Instantiate(
                PrefabFolder.BattleField + @"/" + PrefabName.strWallCrashes,
    this.transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            var setMaterial = deadAnimation.GetComponent<SetMaterial>();
            setMaterial.SetMaterialByInstance(this.renderer.material);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("BrickHealth: {0} hit {1}", other.name, this.name));
        if (this.health <= 0) { return; }

        var bulletScript = other.GetComponentInParent<BulletBase>();
        if (bulletScript == null) { return; }

        var defence = GetDefenceRule(bulletScript);

        if (!defence)
        {
            this.health -= bulletScript.damage;
            this.renderer.material = manager.GetMaterial(initialMaterial, (int)(health / 25));
        }

        bulletScript.Destroy(this);
    }

    private bool GetDefenceRule(BulletBase bulletScript)
    {
        {
            var shockgunBullet = bulletScript as ShockGunBullet;
            if (shockgunBullet != null)
            {
                if (shockgunBullet.id % 5 != 0)
                { return true; }
            }
        }
        {
            if (this.initialMaterial == MaterialName.strTerrWall_G_0)
            {
                var lightningBullet = bulletScript as LightningBullet;
                if (lightningBullet == null)
                { return true; }
            }
        }
        return false;
    }

    public void AllDead()
    {
        Destroy(deadAnimation, 5);
    }
}
