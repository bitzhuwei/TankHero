using UnityEngine;
using System.Collections;

public class Brick4Assembly : Block {

    public enum PrefabOption
    {
        Brick,
    }
    public enum MaterialOption
    {
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
        this.prefabChanged += Brick4Assembly_prefabChanged;
        this.materialChanged += Brick4Assembly_materialChanged;
        base.materialName = material.ToString();
        base.prefabName = prefab.ToString();
        base.Awake();
    }

    void Brick4Assembly_materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly)
    {
        this.material = (MaterialOption)System.Enum.Parse(typeof(MaterialOption), materialName);
        this.lastMaterial = this.material;
    }

    void Brick4Assembly_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
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

    protected override void ArrangePrefab()
    {
        var trans = this.transform;
        var count = trans.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(trans.GetChild(i).gameObject);
        }
        for (int i = 0; i < 4; i++)
        {
            var obj = ResourcesManager.Instantiate(prefabFolder + @"/" + 
                this.prefab.ToString());
            var brick = obj as GameObject;
            if (brick != null)
            {
                brick.name = string.Format("{0}({1})", this.prefab, i);
                brick.transform.parent = trans;
                brick.transform.localPosition = positions[i];
                brick.transform.localRotation = trans.localRotation;
                brick.transform.localScale = trans.localScale;
            }
        }
    }
    static readonly Vector3[] positions = new Vector3[]
    {
        new Vector3(0.25f,0,0.25f),
        new Vector3(0.25f,0,-0.25f),
        new Vector3(-0.25f,0,0.25f),
        new Vector3(-0.25f,0,-0.25f),
    };
}
