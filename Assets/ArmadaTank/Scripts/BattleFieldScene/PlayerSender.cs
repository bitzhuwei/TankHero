using UnityEngine;
using System.Collections;

public class PlayerSender : MonoBehaviour {
    private WorkshopConfigLoader workshopConfigLoader;
    private GameObject player;

	// Use this for initialization
	void Start () {
        var obj = GameObject.FindGameObjectWithTag(Tags.WorkshopConfig);
        this.workshopConfigLoader = obj.GetComponent<WorkshopConfigLoader>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!player)
        {
            var newPlayer = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Player");
            newPlayer.transform.position = new Vector3(-2, 0, -6);
            var config = this.workshopConfigLoader.config;
            var armor = newPlayer.GetComponentInChildren<Armor>();
            armor.prefab = (Armor.PrefabOption)config.boughtArmor;
            var damage = newPlayer.GetComponentInChildren<DamageLevel>();
            damage.prefab = (DamageLevel.PrefabOption)config.boughtDamage;
            var reloadTime = newPlayer.GetComponentInChildren<ReloadTimeLevel>();
            reloadTime.prefab = (ReloadTimeLevel.PrefabOption)config.boughtReloadTime;
            var addMovementSpeed = newPlayer.GetComponentInChildren<AddMovementSpeed>();
            addMovementSpeed.prefab = (AddMovementSpeed.PrefabOption)config.boughtMovementSpeed;
            var projectileSpeed = newPlayer.GetComponentInChildren<ProjectileSpeed>();
            projectileSpeed.prefab = (ProjectileSpeed.PrefabOption)config.boughtProjectileSpeed;
            var headGun = newPlayer.GetComponentInChildren<HeadGun>();
            headGun.prefab = config.currentWeapon;
            this.player = newPlayer;
        }
	}
}
