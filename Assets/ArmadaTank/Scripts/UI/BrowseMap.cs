using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public class BrowseMap : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BrowseMap_Click()
    {
        var dlg = new OpenFileDialog();
        if (dlg.ShowDialog())
        {
            var mapFile = dlg.Filename;
            var mapXml = XElement.Load(mapFile);
            var camera = GameObject.FindGameObjectWithTag(Tags.MainCamera).camera;
            foreach (var item in mapXml.Elements())
            {
                var gameObj = Serialize.Load4MapEditor(item);
                gameObj.AddComponent<Serialize>();
                var script = gameObj.AddComponent<AdsorbToCross>();
                script.putDown = true;
                script.mainCamera = camera;
            }
        }


    }
}
