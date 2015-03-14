using UnityEngine;
using System.Collections;

public class MaterialIndex : MonoBehaviour {

    public SelectMaterial selectMaterialScript;
    private int lastIndex = -1;
    UnityEngine.UI.InputField inputField;
    // Use this for initialization
    void Start()
    {
        inputField = this.GetComponent<UnityEngine.UI.InputField>();
    }

    // Update is called once per frame
    public void UpdateValue()
    {
        var value = 0;
        if (int.TryParse(inputField.text, out value))
        {
            if (value != lastIndex)
            {
                lastIndex = selectMaterialScript.SetIndex(value);
            }
        }
        else
        {
            inputField.text = string.Empty;
        }
    }
}
