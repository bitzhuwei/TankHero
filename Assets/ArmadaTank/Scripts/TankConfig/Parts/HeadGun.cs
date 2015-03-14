using UnityEngine;
using System.Collections;

public class HeadGun : Part
{

    public enum PrefabOption
    {
        HeadGun_Canon,
        HeadGun_Minigun,
        HeadGun_Rocket,
        HeadGun_Lightning,
        HeadGun_Firethrower,
        HeadGun_ShockGun,
    }

    public enum MaterialOption
    {
        Tank_Head_Green,
        Tank_Head_Green_p2,
    }
    public PrefabOption prefab;
    private PrefabOption lastPrefab;
    public MaterialOption material;
    private MaterialOption lastMaterial;

    private static string[] strPrefabOptions;
    private static string[] strMaterialOptions;

 

    private ReloadTimeLevel reloadTimeLevelScript;
    //private float lastReloadTimeLevelUpdatingTime;
    public float reloadTime
    {
        get { return _reloadTime; }
        protected set
        {
            if (_reloadTime != value)
            {
                if (reloadTimeChanged != null)
                { reloadTimeChanged(value); }
            }
            _reloadTime = value;
        }
    }
    private float _reloadTime;
    public event System.Action<float> reloadTimeChanged;

    private DamageLevel damageLevelScript;
    //private float lastDamageLevelUpdatingTime;
    public float damage
    {
        get { return _damage; }
        protected set
        {
            if (_damage != value)
            {
                if (damageChanged != null)
                { damageChanged(value); }
            }
            _damage = value;
        }
    }

    private float _damage;
    public event System.Action<float> damageChanged;

    protected override void Awake()
    {
        this.reloadTimeLevelScript = this.GetComponent<ReloadTimeLevel>();
        this.damageLevelScript = this.GetComponent<DamageLevel>();
        this.reloadTimeLevelScript.prefabChanged += reloadTimeLevelScript_prefabChanged;
        this.damageLevelScript.prefabChanged += damageLevelScript_prefabChanged;
        this.prefabChanged += HeadGun_prefabChanged;
        this.materialChanged += HeadGun_materialChanged;
        base.materialName = material.ToString();
        base.prefabName = prefab.ToString();
        base.Awake();
    }

    void HeadGun_materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly)
    {
        this.material = (MaterialOption)System.Enum.Parse(typeof(MaterialOption), materialName);
        this.lastMaterial = this.material; 
    }

    void HeadGun_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
    {
        this.prefab = (PrefabOption)System.Enum.Parse(typeof(PrefabOption), prefabName);
        this.lastPrefab = this.prefab;
        {
            var level = this.reloadTimeLevelScript.value;
            this.reloadTime = WeaponDict.GetReloadTime(prefab, level);
        }
        {
            var level = this.damageLevelScript.value;
            this.damage = WeaponDict.GetDamage(prefab, level);
        }
    }

    void damageLevelScript_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
    {
        var level = this.damageLevelScript.value;
        this.damage = WeaponDict.GetDamage(prefab, level);
    }

    void reloadTimeLevelScript_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
    {
        var level = this.reloadTimeLevelScript.value;
        this.reloadTime = WeaponDict.GetReloadTime(prefab, level);
    }



    protected override void Update()
    {
        if (this.lastPrefab != this.prefab)
        {
            base.prefabName = this.prefab.ToString();
            this.lastPrefab = this.prefab;
        }
        if (this.lastMaterial != this.material)
        {
            base.materialName = this.material.ToString();
            this.lastMaterial = this.material;
        }
        base.Update();
    }

    protected override string[] DoGetMaterialOptions()
    {
        if (strMaterialOptions == null)
        {
            strMaterialOptions = System.Enum.GetNames(typeof(MaterialOption));
        }
        return strMaterialOptions;
    }
    protected override string[] DoGetPrefabOptions()
    {
        if (strPrefabOptions == null)
        {
            strPrefabOptions = System.Enum.GetNames(typeof(PrefabOption));
        }
        return strPrefabOptions;
    }
    public string GetBulletName()
    {
        return string.Format("bullet_{0}", this.prefab.ToString().Substring("HeadGun_".Length));
    }

    public string GetAudioClipName()
    {
        var result = string.Empty;
        switch (this.prefab)
        {
            case HeadGun.PrefabOption.HeadGun_Canon:
                result = "A_otcannon";
                break;
            case HeadGun.PrefabOption.HeadGun_Minigun:
                result = "A_otcannon";
                break;
            case HeadGun.PrefabOption.HeadGun_Rocket:
                result = "an_misslaun";
                break;
            case HeadGun.PrefabOption.HeadGun_Lightning:
                result = "an_light-3";
                break;
            case HeadGun.PrefabOption.HeadGun_Firethrower:
                result = "an_fire";
                break;
            case HeadGun.PrefabOption.HeadGun_ShockGun:
                result = "an_shock";
                break;
            default:
                break;
        }

        return result;
    }
}
