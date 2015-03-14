using UnityEngine;
using System.Collections;

public class EventPAssembly : Block
{
    public enum PrefabOption
    {
        Event_P_Up,
        Event_P_Down,
    }

    public enum MaterialOption
    {
        EventObject,
    }
    public PrefabOption prefab;
    private PrefabOption lastPrefab;
    public MaterialOption material;
    private MaterialOption lastMaterial;

    private static string[] strPrefabOptions;
    private static string[] strMaterialOptions;
    protected override void Awake()
    {
        this.prefabChanged += EventPAssembly_prefabChanged;
        this.materialChanged += EventPAssembly_materialChanged;
        base.materialName = material.ToString();
        base.prefabName = prefab.ToString();
        base.Awake();
    }

    void EventPAssembly_materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly)
    {
        this.material = (MaterialOption)System.Enum.Parse(typeof(MaterialOption), materialName);
        this.lastMaterial = this.material; 
    }

    void EventPAssembly_prefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly)
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

    static readonly PrefabOption[] childPrefabs = new PrefabOption[] { PrefabOption.Event_P_Up, PrefabOption.Event_P_Down };
    protected override void ArrangePrefab()
    {
        var trans = this.transform;
        var count = trans.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < 2; i++)
        {
            var obj = ResourcesManager.Instantiate(prefabFolder + @"/" + childPrefabs[i].ToString());
            var brick = obj as GameObject;
            if (brick != null)
            {
                brick.name = string.Format("{0}", childPrefabs[i]);
                brick.transform.position = trans.position;
                brick.transform.rotation = trans.rotation;
                brick.transform.localScale = trans.localScale;
                brick.transform.parent = trans;
            }
        }
    }
}
