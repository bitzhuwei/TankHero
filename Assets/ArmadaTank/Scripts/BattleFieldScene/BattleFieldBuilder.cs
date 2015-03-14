using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public class BattleFieldBuilder : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        string path = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            path = Application.dataPath;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            path = Application.dataPath;
        }
        var selectMapManager = GameObject.FindGameObjectWithTag(Tags.SelectMapManager);
        var manager = selectMapManager.GetComponent<SelectMapManager>();
        var filename = string.Format(@"maps/{0}{1}.xml", manager.selectedEpisode, manager.selectedMap);

        var mapXml = XElement.Load(path + @"/" + filename);
        foreach (var item in mapXml.Elements())
        {
            var gameObj = Serialize.Load4BattleField(item);
            if (gameObj.name == typeof(ObstacleAssembly).Name
                || gameObj.name == typeof(EventBuildingAssembly).Name
                || gameObj.name == typeof(EventPAssembly).Name
                || gameObj.name == typeof(FlagAssembly).Name)
            {
                gameObj.isStatic = true;
                gameObj.transform.localScale = new Vector3(0.96f, 0.96f, 0.96f);
            }
            else if (gameObj.name == typeof(Brick4Assembly).Name)
            {
                gameObj.isStatic = true;
                gameObj.transform.localScale = new Vector3(0.96f, 0.96f, 0.96f);
                var addMeshColliderToDTMs = gameObj.GetComponentsInChildren<AddMeshColliderToDTM>();
                if (addMeshColliderToDTMs != null)
                {
                    foreach (var script in addMeshColliderToDTMs)
                    {
                        script.enabled = true;
                    }
                }
                gameObj.AddComponent<Brick4Health>();

            }
        }
        Destroy(selectMapManager);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
