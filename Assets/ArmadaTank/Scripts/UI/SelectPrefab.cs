using UnityEngine;
using System.Collections;

public class SelectPrefab : MonoBehaviour
{

    private GameObject SelectedObjectManager;
    private SelectedObjectManager manager;
    private UnityEngine.UI.Text buttonText;
    private GameObject currentObject;
    private IConfig currentConfig;
    private string[] prefabOptions;
    private int prefabIndex;

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
            prefabIndex = 0;
            currentConfig.SetPrefab(prefabOptions[prefabIndex]);
            UpdateText();
        }
    }

    public void SelectPrefab_Click()
    {
        var current = this.manager.GetCurrent();
        if (current != currentObject)
        {
            UpdateCurrentObject();
        }
        else
        { prefabIndex++; }
        if (prefabIndex >= prefabOptions.Length) { prefabIndex = 0; }
        currentConfig.SetPrefab(prefabOptions[prefabIndex]);
        UpdateText();
    }

    private void UpdateText()
    {
        if (this.currentObject == null)
        {
            this.buttonText.text = string.Format("Prefab: {0}", "null");
        }
        else
        {
            this.buttonText.text = string.Format("Prefab: {0} {1}/{2}", this.currentConfig.GetPrefab(),
                prefabIndex,
                prefabOptions.Length);
        }
    }

    private void UpdateCurrentObject()
    {
        var current = this.manager.GetCurrent();
        if (current == null)
        {
            this.currentObject = null;
            this.currentConfig = null;
            this.prefabOptions = null;
            this.prefabIndex = 0;
        }
        else
        {
            IConfig config = current.GetComponent<AssemblyConfig>();

            this.currentObject = current;
            this.currentConfig = config;
            this.prefabOptions = config.GetPrefabOptions();
            this.prefabIndex = 0;
        }
    }

    public int SetIndex(int value)
    {
        if (value < 0) { value = 0; }
        if (value >= prefabOptions.Length) { value = prefabOptions.Length - 1; }
        prefabIndex = value;
        currentConfig.SetPrefab(prefabOptions[prefabIndex]);
        UpdateText();
        return value;
    }
}
