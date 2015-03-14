using UnityEngine;
using System.Collections;

public class Brick1Assembly : Block {
    public enum PrefabOption
    {
        Brick,
    }

    public enum MaterialOption
    {
        Terr_01,
        Terr_04,
        Terr_05_ice,
        Terr_05_snow,
        Terr_05_snow_B,
        Terr_06,
        TerrWall_C_0,
        TerrWall_C_1,
        TerrWall_C_2,
        TerrWall_C_3,
        TerrWall_G_0,
        TerrWall_R_0,
        TerrWall_R_1,
        TerrWall_R_2,
        TerrWall_R_3,
        TerrWall_W_0,
        TerrWall_W_1,
        TerrWall_W_2,
        TerrWall_W_3,
        TerrWall_Y_0,
        TerrWall_Y_1,
        TerrWall_Y_2,
        TerrWall_Y_3,
    }


    public PrefabOption prefab;
    private PrefabOption lastPrefab;
    public MaterialOption material;
    private MaterialOption lastMaterial;

    private static string[] strPrefabOptions;
    private static string[] strMaterialOptions;

    protected override void Awake()
    {
        this.prefabChanged += Brick1Assembly_prefabChanged;
        this.materialChanged += Brick1Assembly_materialChanged;
        base.materialName = material.ToString();
        base.prefabName = prefab.ToString();
        base.Awake();
    }

    void Brick1Assembly_materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly)
    {
        this.material = (MaterialOption)System.Enum.Parse(typeof(MaterialOption), materialName);
        this.lastMaterial = this.material; 
    }

    void Brick1Assembly_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
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
