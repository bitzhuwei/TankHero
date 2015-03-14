using UnityEngine;
using System.Collections;

public class ProjectileSpeed : Part
{

    public enum PrefabOption
    {
        None = 0,
        Add_ProjectileSpeed = 1,
        Add_ProjectileSpeed_II = 2,
        Add_ProjectileSpeed_III = 3,
        Add_ProjectileSpeed_IIII = 4,
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

    public int value { get; protected set; }
    protected override void Awake()
    {
        this.prefabChanged += ProjectileSpeed_prefabChanged;
        this.materialChanged += ProjectileSpeed_materialChanged;
        base.materialName = material.ToString();
        base.prefabName = prefab.ToString();
        base.Awake();
    }

    void ProjectileSpeed_materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly)
    {
        this.material = (MaterialOption)System.Enum.Parse(typeof(MaterialOption), materialName);
        this.lastMaterial = this.material; 
    }

    void ProjectileSpeed_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
    {
        this.prefab = (PrefabOption)System.Enum.Parse(typeof(PrefabOption), prefabName);
        this.lastPrefab = this.prefab;
        this.value = (int)this.prefab;
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
}
