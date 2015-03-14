using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public class SaveMap : MonoBehaviour
{

    public UnityEngine.UI.InputField txtFilename;
    private bool fading = false;
    private UnityEngine.UI.Text thisText;
    private float fadingInterval = 1;
    private float passedFadingInterval;
    // Use this for initialization
    void Start()
    {
        thisText = this.GetComponentInChildren<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            passedFadingInterval += Time.deltaTime;
            if(passedFadingInterval>=fadingInterval)
            {
                this.thisText.text = "Save";
                this.fading = false;
                passedFadingInterval = 0;
            }
        }
    }

    public void SaveMap_Click()
    {

        var result = new XElement("Map");
        var objs = GameObject.FindObjectsOfType<GameObject>();
        foreach (var item in objs)
        {
            var serializeScript = item.GetComponent<Serialize>();
            if (serializeScript == null) { continue; }
            result.Add(serializeScript.ToXElement());
        }

        result.Save(string.Format("Map_{0}_{1:yyyyMMdd_HHmmss}.xml",
            txtFilename.text,
            System.DateTime.Now));
        fading = true;
        this.thisText.text = "Saved!";
        //System.Diagnostics.Process.Start("notepad", )
    }
}
