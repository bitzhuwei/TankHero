using UnityEngine;
using System.Collections;

public class ResourcesManager
{
    private static System.Collections.Generic.Dictionary<string, Material> materialDict;
    private const string materialFolder = @"Materials/";
    private static System.Collections.Generic.Dictionary<string, GameObject> prefabDict;
    private const string prefabFolder = @"Prefabs/";
    private static System.Collections.Generic.Dictionary<string, Texture2D> Texture2DDict;
    private const string texture2DFolder = @"Texture2Ds/";

    public static Texture2D GetTexture2D(string texture2DName)
    {
        if (string.IsNullOrEmpty(texture2DName)) { return null; }

        if (Texture2DDict == null)
        {
            Texture2DDict = new System.Collections.Generic.Dictionary<string, Texture2D>();
        }

        if (!Texture2DDict.ContainsKey(texture2DName))
        {
            var texture2D = Resources.Load<Texture2D>(texture2DFolder + texture2DName);
            if (texture2D != null)
            { Texture2DDict.Add(texture2DName, texture2D); }
        }

        Texture2D result = null;
        if (Texture2DDict.TryGetValue(texture2DName, out result))
        { return result; }
        else
        { return null; }
    }


    public static Material GetMaterial(string materialName)
    {
        if (string.IsNullOrEmpty(materialName)) { return null; }

        if (materialDict == null)
        {
            materialDict = new System.Collections.Generic.Dictionary<string, Material>();
        }

        if (!materialDict.ContainsKey(materialName))
        {
            var material = Resources.Load<Material>(materialFolder + materialName);
            if (material != null)
            { materialDict.Add(materialName, material); }
        }

        Material result = null;
        if (materialDict.TryGetValue(materialName, out result))
        { return result; }
        else
        { return null; }
    }

    public static GameObject Instantiate(string prefabName)
    {
        if (string.IsNullOrEmpty(prefabName)) { return null; }

        if (prefabDict == null)
        {
            prefabDict = new System.Collections.Generic.Dictionary<string, GameObject>();
        }

        if (!prefabDict.ContainsKey(prefabName))
        {
            var prefab = Resources.Load<GameObject>(prefabFolder + prefabName);
            if (prefab != null)
            { prefabDict.Add(prefabName, prefab); }
        }

        GameObject obj = null;
        if (prefabDict.TryGetValue(prefabName, out obj))
        {
            var result = Object.Instantiate(obj) as GameObject;
            return result;
        }
        else
        { return null; }
    }

    public static GameObject Instantiate(string prefabName, Vector3 position, Quaternion rotation)
    {
        if (string.IsNullOrEmpty(prefabName)) { return null; }

        if (prefabDict == null)
        {
            prefabDict = new System.Collections.Generic.Dictionary<string, GameObject>();
        }

        if (!prefabDict.ContainsKey(prefabName))
        {
            var prefab = Resources.Load<GameObject>(prefabFolder + prefabName);
            if (prefab != null)
            { prefabDict.Add(prefabName, prefab); }
        }

        GameObject obj = null;
        if (prefabDict.TryGetValue(prefabName, out obj))
        {
            var result = Object.Instantiate(obj, position, rotation) as GameObject;
            return result;
        }
        else
        {
            Debug.LogError(string.Format("the asset [{0}] is not found.", prefabName));
            return null; 
        }
    }


}
