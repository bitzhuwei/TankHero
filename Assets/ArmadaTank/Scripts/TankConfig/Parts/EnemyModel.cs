using UnityEngine;
using System.Collections;

public class EnemyModel : Part
{
    public enum PrefabOption
    {
        Boss1,
        Boss2,
        Tank_Bron,
        Tank_DarkBoss,
        Tank_Easy,
        Tank_Electro,
        Tank_Fire,
        Tank_Hard,
        Tank_Normal,
        Tank_Nuc,
        Tank_Raket,
        TankRotGun,
    }

    public enum MaterialOption
    {
        Tank_Enemy512,
        Tank_Enemy512_Blue,
        Tank_Enemy512_DARK,
        Tank_Enemy512_INV,
        Tank_Enemy512_red,
        Invisible,
    }
    public PrefabOption prefab;
    private PrefabOption lastPrefab;
    public MaterialOption material;
    private MaterialOption lastMaterial;

    private static string[] strPrefabOptions;
    private static string[] strMaterialOptions;

    protected override void Awake()
    {
        this.prefabChanged += EnemyModel_prefabChanged;
        this.materialChanged += EnemyModel_materialChanged;
        base.materialName = material.ToString();
        base.prefabName = prefab.ToString();
        base.Awake();
    }

    void EnemyModel_materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly)
    {
        this.material = (MaterialOption)System.Enum.Parse(typeof(MaterialOption), materialName);
        this.lastMaterial = this.material; 
    }

    void EnemyModel_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
    {
        this.prefab = (PrefabOption)System.Enum.Parse(typeof(PrefabOption), prefabName);
        this.lastPrefab = this.prefab; 
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


    /// <summary>
    /// TankCode is addmovementspeed, armor, addDamage, add projectilespeed ...
    /// </summary>
    public string TankCode { get; protected set; }
    public void SetTankCode(string tankCode)
    {
        if (string.IsNullOrEmpty(tankCode)) { return; }

        this.TankCode = tankCode;
        //TODO: make a stronger parser here.
        switch (tankCode[0])
        {
            case '1':
                this.prefab = PrefabOption.Tank_Easy;
                break;
            case '2':
                this.prefab = PrefabOption.Tank_Normal;
                break;
            case '3':
                this.prefab = PrefabOption.Tank_Hard;
                break;
            case '4':
                this.prefab = PrefabOption.Tank_Nuc;
                break;
            case '5':
                this.prefab = PrefabOption.Tank_Raket;
                break;
            case '6':
                this.prefab = PrefabOption.Tank_Electro;
                break;
            case '7':
                this.prefab = PrefabOption.Tank_Bron;
                break;
            case '8':
                this.prefab = PrefabOption.Tank_Fire;
                break;
            case '9':
                this.prefab = PrefabOption.TankRotGun;
                break;
            default:
                break;
        }

        if(tankCode.Contains("I"))
        {
            this.material = MaterialOption.Invisible;
        }
    }

    public float GetReloadTime()
    {
        var level = 0;
        var reloadTime = WeaponDict.GetReloadTime(prefab, level);
        return reloadTime;
    }

    public string GetBulletName()
    {
        var result = "Bullet_NotSetYet4Enemy";
        switch (this.prefab)
        {
            case PrefabOption.Boss1:
                result = "bullet_Canon";
                break;
            case PrefabOption.Boss2:
                result = "bullet_Firethrower";
                break;
            case PrefabOption.Tank_Bron:
                result = "bullet_Canon";
                break;
            case PrefabOption.Tank_DarkBoss:
                result = "bullet_Canon";
                break;
            case PrefabOption.Tank_Easy:
                result = "bullet_Canon";
                break;
            case PrefabOption.Tank_Electro:
                break;
            case PrefabOption.Tank_Fire:
                result = "bullet_Firethrower";
                break;
            case PrefabOption.Tank_Hard:
                result = "bullet_Canon";
                break;
            case PrefabOption.Tank_Normal:
                result = "bullet_Canon";
                break;
            case PrefabOption.Tank_Nuc:
                //result = "bullet_Canon";
                break;
            case PrefabOption.Tank_Raket:
                result = "bullet_Rocket";
                break;
            case PrefabOption.TankRotGun:
                result = "bullet_Minigun";
                break;
            default:
                break;
        }
        return result;
    }

    public string GetAudioClipName()
    {
        var result = string.Empty;
        result = "A_otcannon";
        switch (this.prefab)
        {
            case PrefabOption.Boss1:
                result = "A_otcannon";
                break;
            case PrefabOption.Boss2:
                result = "an_fire";
                break;
            case PrefabOption.Tank_Bron:
                result = "A_otcannon";
                break;
            case PrefabOption.Tank_DarkBoss:
                result = "A_otcannon";
                break;
            case PrefabOption.Tank_Easy:
                result = "A_otcannon";
                break;
            case PrefabOption.Tank_Electro:
                result = "A_otcannon";
                break;
            case PrefabOption.Tank_Fire:
                result = "an_fire";
                break;
            case PrefabOption.Tank_Hard:
                result = "A_otcannon";
                break;
            case PrefabOption.Tank_Normal:
                result = "A_otcannon";
                break;
            case PrefabOption.Tank_Nuc:
                result = "A_otcannon";
                break;
            case PrefabOption.Tank_Raket:
                result = "an_misslaun";
                break;
            case PrefabOption.TankRotGun:
                result = "A_otcannon";
                break;
            default:
                break;
        }

        return result;
    }

    public float GetDamage()
    {
        //var code = this.TankCode;
        var level = 0;
        var reloadTime = WeaponDict.GetDamage(prefab, level);
        return reloadTime;
    }

    public int GetProjectileLevel()
    {
        //var code = this.TankCode;
        return 0;
    }

    public float GetHealth()
    {
        //var code = this.TankCode;
        return 99;
    }
}
