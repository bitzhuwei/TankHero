using UnityEngine;
using System.Collections;

public class SetMaterial : MonoBehaviour {

    public void SetMaterialByName(string materialName)
    {
        var material = ResourcesManager.GetMaterial(materialName);
        SetMaterialByInstance(material);
    }
    public void SetMaterialByInstance(Material material)
    {
        var count = this.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            var dtmChild = this.transform.GetChild(i);
            if (dtmChild.renderer != null)
            {
                dtmChild.renderer.material = material;
            }
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
