using UnityEngine;
using System.Collections;

public class FlagAssembly : Block
{

    public enum PrefabOption
    {
        Flag_I,
        Flag_II,
        Flag_III,
    }
    public enum MaterialOption
    {
        Flag_123,
    }

    public PrefabOption prefab;
    private PrefabOption lastPrefab;
    public MaterialOption material;
    private MaterialOption lastMaterial;

    private static string[] strPrefabOptions;
    private static string[] strMaterialOptions;

    protected override void Awake()
    {
        this.prefabChanged += FlagAssembly_prefabChanged;
        this.materialChanged += FlagAssembly_materialChanged;
        base.materialName = material.ToString();
        base.prefabName = prefab.ToString();
        base.Awake();
    }

    void FlagAssembly_materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly)
    {
        this.material = (MaterialOption)System.Enum.Parse(typeof(MaterialOption), materialName);
        this.lastMaterial = this.material; 
    }

    void FlagAssembly_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
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
}

