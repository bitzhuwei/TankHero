using UnityEngine;
using System.Collections;

public interface IConfig 
{
    string[] GetPrefabOptions();
    string[] GetMaterialOptions();
    string GetPrefab();
    string GetMaterial();
    void SetPrefab(string name);
    void SetMaterial(string name);
}
