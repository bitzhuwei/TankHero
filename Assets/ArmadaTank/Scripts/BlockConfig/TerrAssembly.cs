using UnityEngine;
using System.Collections;

public class TerrAssembly : Block
{
    public enum PrefabOption
    {
        Abatis,
        Terr_0,
        Terr_1,
        Terr_2,
        Terr_3,
        Terr_4,
        Terr_5,
        Terr_6,
        Terr_7,
        Terr_8,
        Terr_9,
        Terr_10,
        Terr_11,
        Terr_12,
        Terr_13,
        Terr_14,
        Terr_15,
        Terr_16,
        Terr_17,
        Terr_18,
        terr_19,
        terr_20,
        Terr_21,
        terr_22,
        terr_23,
        terr_24,
        terr_25,
        terr_26,
        Terr_27,
        Terr_28,
        Terr_29,
        Terr_30,
        Terr_31,
        Terr_32,
        Terr_33,
        terr_34,
        terr_35,
        terr_36,
        terr_37,
        terr_38,
        terr_41,
        Terr_42,
        Terr_43,
        Terr_44,
        terr_45,
        terr_46,
        terr_47,
        terr_48,
        terr_49,
        terr_50,
        Terr_51,
        Terr_52,
        Terr_53,
        Terr_54,
        Terr_55,
        terr_56,
        terr_57,
        terr_58,
        terr_59,
        Terr_61,
        terr_62,
        terr_63,
        Terr_64,
        Terr_65,
        Terr_66,
        Terr_67,
        Terr_68,
        Terr_69,
        Terr_70,
        Terr_71,
        Terr_74,
        terr_75,
        terr_76,
        terr_77,
        terr_78,
        terr_79,
        Terr_81,
        Terr_82,
        Terr_83,
        Terr_84,
        Terr_85,
        Terr_86,
        Terr_87,
        Terr_88,
        terr_90,
        terr_91,
        terr_92,
        terr_93,
        terr_94,
        Terr_95,
        Terr_96,
        Terr_97,
        Terr_98,
        Terr_99,
        Terr_100,
        Terr_101,
        Terr_102,
        Terr_103,
        Terr_104,
        Terr_105,
        Terr_106,
        Terr_107,
        Terr_108,
        Terr_109,
        Terr_110,
    }

    public enum MaterialOption
    {
        Terr_Dung,
        Terr_Egypt,
        Terr_Egypt_Obj,
        Terr_Fire,
        Terr_Forest_Pac,
        Terr_Forest_Pac_Obj,
        Terr_Grot,
        Terr_Ice,
        Terr_Sand_Pac,
        Terr_Sand_Pac_Obj,
        Terr_Snow,
        Terr_Tundra,
    }

    public PrefabOption prefab;
    private PrefabOption lastPrefab;
    public MaterialOption material;
    private MaterialOption lastMaterial;

    private static string[] strPrefabOptions;
    private static string[] strMaterialOptions;

    protected override void Awake()
    {
        this.prefabChanged += TerrAssembly_prefabChanged;
        this.materialChanged += TerrAssembly_materialChanged;
        base.materialName = material.ToString();
        base.prefabName = prefab.ToString();
        base.Awake();
    }

    void TerrAssembly_materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly)
    {
        this.material = (MaterialOption)System.Enum.Parse(typeof(MaterialOption), materialName);
        this.lastMaterial = this.material; 
    }

    void TerrAssembly_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
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
