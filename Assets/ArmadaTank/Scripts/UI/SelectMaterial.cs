using UnityEngine;
using System.Collections;

public class SelectMaterial : MonoBehaviour {

    private GameObject SelectedObjectManager;
    private SelectedObjectManager manager;
    private UnityEngine.UI.Text buttonText;
    private GameObject currentObject;
    private IConfig currentConfig;
    private string[] materialOptions;
    private int materialIndex;

    // Use this for initialization
    void Start()
    {
        this.SelectedObjectManager = GameObject.FindGameObjectWithTag(Tags.SelectedObjectManager);
        this.manager = this.SelectedObjectManager.GetComponent<SelectedObjectManager>();
        this.buttonText = this.GetComponentInChildren<UnityEngine.UI.Text>();

    }

    // Update is called once per frame
    void Update()
    {
        var current = this.manager.GetCurrent();
        if (current != currentObject)
        {
            UpdateCurrentObject();
            materialIndex = 0;
            currentConfig.SetMaterial(materialOptions[materialIndex]);
            UpdateText();
        }
    }

    private void UpdateText()
    {
        if (this.currentObject == null)
        {
            this.buttonText.text = string.Format("Material: {0}", "null");
        }
        else
        {
            this.buttonText.text = string.Format("Material: {0} {1}/{2}", this.currentConfig.GetMaterial(),
              materialIndex, materialOptions.Length);
        }
    }

    public void SelectMaterial_Click()
    {
        var current = this.manager.GetCurrent();
        if (current != currentObject)
        {
            UpdateCurrentObject();
        }
        else
        { materialIndex++; }
        if (materialIndex >= materialOptions.Length) { materialIndex = 0; }
        currentConfig.SetMaterial(materialOptions[materialIndex]);
        UpdateText();
    }

    private void UpdateCurrentObject()
    {
        var current = this.manager.GetCurrent();
        if (current == null)
        {
            this.currentObject = null;
            this.currentConfig = null;
            this.materialOptions = null;
            this.materialIndex = 0;
        }
        else
        {
            IConfig config = current.GetComponent<AssemblyConfig>();

            this.currentObject = current;
            this.currentConfig = config;
            this.materialOptions = config.GetMaterialOptions();
            this.materialIndex = 0;
        }
    }

    public int SetIndex(int value)
    {
        if (value < 0) { value = 0; }
        if (value >= materialOptions.Length) { value = materialOptions.Length - 1; }
        materialIndex = value;
        currentConfig.SetMaterial(materialOptions[materialIndex]);
        UpdateText();
        return value;
    }
}
