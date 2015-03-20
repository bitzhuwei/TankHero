using UnityEngine;
using System.Collections;

public class WorkshopConfigLoader : MonoBehaviour
{
    const string defaultWorkshopContent = @"money:0;
currentWeapon:HeadGun_Canon;
//HeadGun_Minigun;
//HeadGun_Rocket;
//HeadGun_Lightning;
//HeadGun_Firethrower;
//HeadGun_ShockGun;
boughtWeapon:HeadGun_Canon;//HeadGun_Canon,HeadGun_Minigun,HeadGun_Rocket,HeadGun_Lightning,HeadGun_Firethrower,HeadGun_ShockGun;
boughtArmor:0;//0 1 2 3 4
boughtMovementSpeed:0;//0 1 2 3 4
boughtReloadTime:0;//0 1 2 3 4
boughtProjectileSpeed:0;//0 1 2 3 4
boughtDamage:0;//0 1 2 3 4
weaponPrice:100,200,300,400,500,600;
armorPrice:100,200,400,800;
movementSpeedPrice:100,200,400,800;
reloadTimePrice:100,200,400,800;
projectileSpeedPrice:100,200,400,800;
damagePrice:100,200,400,800;
";
    public WorkshopConfig config { get; protected set; }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        var content = FileHelper.Read(ConfigFilenames.workshopConfig);
        if (content == string.Empty)
        {
            FileHelper.Write(ConfigFilenames.workshopConfig, defaultWorkshopContent);
            content = defaultWorkshopContent;
        }
        config = WorkshopConfig.Parse(content);
    }
    // Use this for initialization
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }
}
