using UnityEngine;
using System.Collections;

public class WorkshopSelectWeapon : MonoBehaviour {

    public WorkshopConfigLoader configLoader;
    public HeadGun headGunScript;
    public string weaponPrefabName;
    private HeadGun.PrefabOption prefab;
    private Transform imgGotIt;
    private UnityEngine.UI.Text text;

    void Awake()
    {
        this.imgGotIt = this.transform.FindChild("Image");
        this.text = this.transform.FindChild("Text").GetComponent<UnityEngine.UI.Text>();
    }
	// Use this for initialization
	void Start () {
        prefab = (HeadGun.PrefabOption)System.Enum.Parse(typeof(HeadGun.PrefabOption), weaponPrefabName);	
        if(!this.configLoader.config.boughtWeapons.Contains(prefab))
        {
            this.imgGotIt.gameObject.SetActive(false);
            this.text.text = string.Format("{0}{1}￥{2}",
                this.name, System.Environment.NewLine,
                this.configLoader.config.weaponPriceDict[prefab]);
        }

        if (this.configLoader.config.currentWeapon == prefab)
        {
            this.headGunScript.prefab = prefab;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectWeapon_Click()
    {
        if (!this.configLoader.config.boughtWeapons.Contains(prefab))
        {
            var price = this.configLoader.config.weaponPriceDict[prefab];
            var money = this.configLoader.config.money;
            if (money >= price)
            {
                this.configLoader.config.money -= price;
                this.configLoader.config.boughtWeapons.Add(prefab);
                this.imgGotIt.gameObject.SetActive(true);
                this.text.text = string.Format("{0}{1}",
                    this.name, System.Environment.NewLine);
            }
        }

        if (this.configLoader.config.boughtWeapons.Contains(prefab))
        {
            headGunScript.prefab = prefab;
            this.configLoader.config.currentWeapon = prefab;
        }
    }
}
