using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public class Serialize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private const string strPrefab = "Prefab";
    private const string strMaterial = "Material";
    private const string strPosition= "Position";
    private const string strRotation= "Rotation";
    public XElement ToXElement()
    {
        IConfig config = this.GetComponent<AssemblyConfig>();
        //if(!config.GetType().Name.StartsWith(this.name))
        //{
        //    Debug.LogError(string.Format("{0} should not change its name {1}",
        //        this.name, config.GetType().Name.Substring(0, config.GetType().Name.Length - "Assembly".Length)));
        //}
        var asmName = config.GetType().Name;
        var result = new XElement(asmName.Substring(0, asmName.Length - "Assembly".Length),
            new XElement(strPrefab, config.GetPrefab()),
            new XElement(strMaterial, config.GetMaterial()),
            new XElement(strPosition, this.transform.position),
            //new XAttribute("X", this.transform.position.x),
            //new XAttribute("Y", this.transform.position.y),
            //new XAttribute("Z",this.transform.position.z)),
            new XElement(strRotation, this.transform.rotation.eulerAngles)
                );
        return result;
    }

    public static GameObject Load4BattleField(XElement xml)
    {
        var configTypeName = xml.Name.ToString();
        var gameObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + configTypeName);
        IConfig config = gameObj.GetComponent<AssemblyConfig>();

        var prefab = xml.Element(strPrefab);
        config.SetPrefab(prefab.Value);
        var material = xml.Element(strMaterial);
        config.SetMaterial(material.Value);
        var position = xml.Element(strPosition);
        gameObj.transform.position = FromString(position.Value);
        var rotation = xml.Element(strRotation);
        gameObj.transform.rotation = Quaternion.Euler(FromString(rotation.Value));
        return gameObj;
    }

    public static GameObject Load4MapEditor(XElement xml)
    {
        var configTypeName = xml.Name.ToString();
        var gameObj = ResourcesManager.Instantiate(PrefabFolder.MapEditor + @"/" + configTypeName);
        IConfig config = gameObj.GetComponent<AssemblyConfig>();

        var prefab = xml.Element(strPrefab);
        config.SetPrefab(prefab.Value);
        var material = xml.Element(strMaterial);
        config.SetMaterial(material.Value);
        var position = xml.Element(strPosition);
        gameObj.transform.position = FromString(position.Value);
        var rotation = xml.Element(strRotation);
        gameObj.transform.rotation = Quaternion.Euler(FromString(rotation.Value));
        return gameObj;
    }

    //public static GameObject Load4MapEditor(XElement xml)
    //{
    //    var configTypeName = xml.Name.ToString();
    //    var gameObj = new GameObject(configTypeName);
    //    var assemblyConfig = gameObj.AddComponent(configTypeName) as AssemblyConfig;
    //    assemblyConfig.prefabFolder = PrefabFolder.MapEditor;
    //    IConfig config = assemblyConfig;

    //    var prefab = xml.Element(strPrefab);
    //    config.SetPrefab(prefab.Value);
    //    var material = xml.Element(strMaterial);
    //    config.SetMaterial(material.Value);
    //    var position = xml.Element(strPosition);
    //    gameObj.transform.position = FromString(position.Value);
    //    var rotation = xml.Element(strRotation);
    //    gameObj.transform.rotation = Quaternion.Euler(FromString(rotation.Value));
    //    return gameObj;
    //}

    static readonly char[] splitor = new char[] { '(', ',', ' ', ')', }; 
    static Vector3 FromString(string value)
    {
        var parts = value.Split(splitor, System.StringSplitOptions.RemoveEmptyEntries);
        var x = float.Parse(parts[0]);
        var y = float.Parse(parts[1]);
        var z = float.Parse(parts[2]);
        return new Vector3(x, y, z);
    }
}
