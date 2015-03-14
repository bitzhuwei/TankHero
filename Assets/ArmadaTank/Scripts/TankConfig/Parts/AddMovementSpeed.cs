using UnityEngine;
using System.Collections;

public class AddMovementSpeed : Part
{
    public TankTransform tankTransform;

    public enum PrefabOption
    {
        None,
        Add_MovementSpeed,
        Add_MovementSpeed_II,
        Add_MovementSpeed_III,
        Add_MovementSpeed_IIII,
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
    private static readonly float[] speedUpValue = new float[] { 
        0f, 0.04f,0.08f,0.12f,0.17f
    };
    public float value { get; protected set; }

    protected override void Awake()
    {
        this.tankTransform = this.GetComponentInParent<TankTransform>();
        this.prefabChanged += AddMovementSpeed_prefabChanged;
        this.materialChanged += AddMovementSpeed_materialChanged;
        base.materialName = material.ToString();
        base.prefabName = prefab.ToString();
        base.Awake();
    }

    void AddMovementSpeed_materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly)
    {
        this.material = (MaterialOption)System.Enum.Parse(typeof(MaterialOption), materialName);
        this.lastMaterial = this.material;
    }

    void AddMovementSpeed_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
    {
        this.prefab = (PrefabOption)System.Enum.Parse(typeof(PrefabOption), prefabName);
        this.lastPrefab = this.prefab;
        var possiblePrefabs = DoGetPrefabOptions();
        for (int i = 0; i < possiblePrefabs.Length; i++)
        {
            if (possiblePrefabs[i] == prefabName)
            {
                value = speedUpValue[i];
                break;
            }
        }

        if (this.tankTransform != null)
        { this.tankTransform.AddSpeed(this.value); }
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
