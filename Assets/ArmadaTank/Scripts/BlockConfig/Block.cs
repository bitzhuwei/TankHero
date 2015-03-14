using UnityEngine;
using System.Collections;

public abstract class Block : AssemblyConfig
{

    protected override void ArrangeMaterial()
    {
        var scripts = this.GetComponentsInChildren<SetMaterial>();
        foreach (var item in scripts)
        {
            item.SetMaterialByName(materialName);
        }

        this.lastMaterialName = this.materialName;
    }
    protected override void ArrangePrefab()
    {
        if (string.IsNullOrEmpty(prefabFolder)) { return; }

        var count = this.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

        var prefabName = this.prefabName;
        var obj = ResourcesManager.Instantiate(prefabFolder + @"/" + prefabName);
        var gameObj = obj as GameObject;
        if (gameObj != null)
        {
            gameObj.name = prefabName;
            gameObj.transform.position = this.transform.position;
            gameObj.transform.rotation = this.transform.rotation;
            gameObj.transform.localScale = this.transform.localScale;
            gameObj.transform.parent = this.transform;
        }

        this.lastUpdatingTime = Time.time;
        this.lastPrefabName = prefabName;
    }

}
