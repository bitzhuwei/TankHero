using UnityEngine;
using System.Collections;

public class PlayerSender : MonoBehaviour {

#if UNITY_ANDROID
    public TouchPanelTouching goUp;
    public TouchPanelTouching goDown;
    public TouchPanelTouching goLeft;
    public TouchPanelTouching goRight;
    public TouchPanelTouching goCenter;
#endif

    private OriginalMapLoader mapLoader;
    private WorkshopConfigLoader workshopConfigLoader;
    private GameObject player;

	// Use this for initialization
    void Start()
    {
        {
            var obj = GameObject.FindGameObjectWithTag(Tags.WorkshopConfig);
            this.workshopConfigLoader = obj.GetComponent<WorkshopConfigLoader>();
        }
        {
            var obj = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
            this.mapLoader = obj.GetComponent<OriginalMapLoader>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(!player)
        {
            player = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Player");
            player.transform.position = mapLoader.map.respawnList[0].position;// new Vector3(-2, 0, -6);
            var config = this.workshopConfigLoader.config;
            var armor = player.GetComponentInChildren<Armor>();
            armor.prefab = (Armor.PrefabOption)config.boughtArmor;
            var damage = player.GetComponentInChildren<DamageLevel>();
            damage.prefab = (DamageLevel.PrefabOption)config.boughtDamage;
            var reloadTime = player.GetComponentInChildren<ReloadTimeLevel>();
            reloadTime.prefab = (ReloadTimeLevel.PrefabOption)config.boughtReloadTime;
            var addMovementSpeed = player.GetComponentInChildren<AddMovementSpeed>();
            addMovementSpeed.prefab = (AddMovementSpeed.PrefabOption)config.boughtMovementSpeed;
            var projectileSpeed = player.GetComponentInChildren<ProjectileSpeed>();
            projectileSpeed.prefab = (ProjectileSpeed.PrefabOption)config.boughtProjectileSpeed;
            var headGun = player.GetComponentInChildren<HeadGun>();
            headGun.prefab = config.currentWeapon;
#if UNITY_ANDROID
            var touchState = player.GetComponent<AndroidTouchState>();
            touchState.goUp = goUp;
            touchState.goDown = goDown;
            touchState.goLeft = goLeft;
            touchState.goRight = goRight;
            touchState.goCenter = goCenter;
#endif
       }
	}
}
