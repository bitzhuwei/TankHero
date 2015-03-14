using UnityEngine;
using System.Collections;

public abstract class Part : AssemblyConfig
{
    protected override void ArrangeMaterial()
    {
        foreach (var item in DoGetPrefabOptions())
        {
            var child = this.transform.Find(item);
            if (child != null)
            {
                var scripts = child.GetComponentsInChildren<SetMaterial>();
                foreach (var script in scripts)
                {
                    script.SetMaterialByName(materialName);
                }
            }
        }
        this.lastMaterialName = this.materialName;
    }


    protected override void ArrangePrefab()
    {
        if (string.IsNullOrEmpty(prefabFolder)) { return; }

        var prefabName = this.prefabName;
        var i = 0;
        var possiblePrefabs = DoGetPrefabOptions();
        while (i < possiblePrefabs.Length)
        {
            var child = this.transform.Find(possiblePrefabs[i]);
            if (child == null)
            {
                i++;
            }
            else
            {
                child.parent = null;
                Destroy(child.gameObject);
            }
        }

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
