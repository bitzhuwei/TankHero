using UnityEngine;
using System.Collections;

public class SelectedObjectInfo : MonoBehaviour
{
    public Camera mainCamera;
    private UnityEngine.UI.Text textComponent;

    // Use this for initialization
    void Start()
    {
        this.textComponent = this.GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        var hitting = false;
        foreach (var item in hits)
        {
            if (item.collider.tag == Tags.Plane) { continue; }
            if (item.collider.transform.parent == null) { continue; }
            if (item.collider.transform.parent.parent == null) { continue; }
            var adsorbToCrossScript = item.collider.transform.parent.parent.GetComponent<AdsorbToCross>();
            if (adsorbToCrossScript == null) { continue; }
            IConfig config = adsorbToCrossScript.GetComponent<AssemblyConfig>();
            if (config == null) { continue; }

            hitting = true;

            var builder = new System.Text.StringBuilder();
            builder.AppendLine(string.Format("{0}", adsorbToCrossScript.putDown ? "Mouse over" : "Positioning"));
            builder.AppendLine(string.Format("Object: {0}", config.GetType().Name));
            {
                var prefab = config.GetPrefab();
                var prefabs = config.GetPrefabOptions();
                var prefabIndex = IndexOf(prefab, prefabs);
                builder.AppendLine(string.Format("Prefab: {0}({1}/{2})", prefab, prefabIndex, prefabs.Length));
            }
            {
                var material = config.GetMaterial();
                var materials = config.GetMaterialOptions();
                var materialIndex = IndexOf(material, materials);
                builder.AppendLine(string.Format("Material: {0}({1}/{2})", material, materialIndex, materials.Length));
            }
            {
                var trans = adsorbToCrossScript.transform;
                builder.AppendLine(string.Format("Position:{0}", trans.position));
                builder.AppendLine(string.Format("Local Position:{0}", trans.localPosition));
                builder.AppendLine(string.Format("Rotation:{0}", trans.rotation.eulerAngles));
                builder.AppendLine(string.Format("Local Rotation:{0}", trans.localRotation.eulerAngles));
                builder.AppendLine(string.Format("Scale:{0}", trans.lossyScale));
                builder.AppendLine(string.Format("Local Scale:{0}", trans.localScale));
            }
            this.textComponent.text = builder.ToString();
            break;
        }

        if (!hitting)
        {
            this.textComponent.text = string.Empty;
        }
    }

    private object IndexOf(string str, string[] strings)
    {
        if (strings == null || str == null) { return -1; }
        for (int i = 0; i < strings.Length; i++)
        {
            if (str == strings[i])
            { return i; }
        }
        return -1;
    }

}
