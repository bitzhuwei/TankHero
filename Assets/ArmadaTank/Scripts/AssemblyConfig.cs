using UnityEngine;
using System.Collections;

public abstract class AssemblyConfig : MonoBehaviour, IConfig {

    public string prefabFolder;
    public float lastUpdatingTime { get; set; }
    public event PrefabChanged prefabChanged;
    public event materialChanged materialChanged;

    protected string materialName;
    protected string lastMaterialName;
    protected string prefabName;
    protected string lastPrefabName;

    protected virtual void Awake()
    {
        Check2Arrange();
    }

    // Use this for initialization
    protected virtual void Start()
    {
        //Check2Arrange();
    }

    private void Check2Arrange()
    {
        if (this.lastPrefabName != this.prefabName)
        {
            ArrangePrefab();
            ArrangeMaterial();
            OnPrefabChanged(prefabName, lastPrefabName, this);
            OnMaterialChanged(materialName, lastMaterialName, this);
            this.lastPrefabName = this.prefabName;
            this.lastMaterialName = this.materialName;
        }
        else if (this.lastMaterialName != this.materialName)
        {
            ArrangeMaterial();
            if (materialChanged != null)
            { materialChanged(materialName, lastMaterialName, this); }
            this.lastMaterialName = this.materialName;
        }
    }

    private void OnMaterialChanged(string materialName, string lastMaterialName, AssemblyConfig assemblyConfig)
    {
        if (materialChanged != null)
        { materialChanged(materialName, lastMaterialName, this); }
    }

    private void OnPrefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assemblyConfig)
    {
        if (prefabChanged != null)
        { prefabChanged(prefabName, lastPrefabName, this); }
    }

    protected abstract void ArrangeMaterial();


    protected abstract void ArrangePrefab();


    // Update is called once per frame
    protected virtual void Update()
    {
        Check2Arrange();
    }


    string[] IConfig.GetPrefabOptions()
    {
        return DoGetPrefabOptions();
    }
    protected abstract string[] DoGetPrefabOptions();

    string[] IConfig.GetMaterialOptions()
    {
        return DoGetMaterialOptions();
    }
    protected abstract string[] DoGetMaterialOptions();

    void IConfig.SetPrefab(string name)
    {
        this.prefabName = name;
        Check2Arrange();
    }

    void IConfig.SetMaterial(string name)
    {
        this.materialName = name;
        Check2Arrange();
    }


    string IConfig.GetPrefab()
    {
        return this.prefabName;
    }

    string IConfig.GetMaterial()
    {
        return this.materialName;
    }
}

public delegate void PrefabChanged(string prefabName, string lastPrefabName, AssemblyConfig assembly);
public delegate void materialChanged(string materialName, string lastMaterialName, AssemblyConfig assembly);