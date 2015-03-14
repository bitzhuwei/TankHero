using UnityEngine;
using System.Collections;

public class ShowTankInfo : MonoBehaviour
{
    public WorkshopConfigLoader configLoader;
    public Transform player;
    private TankTransform tankTransform;
    private Armor armor;
    private HeadGun weapon;
    private UnityEngine.UI.Text text;

    void Awake()
    {
        tankTransform = player.GetComponent<TankTransform>();
        armor = player.GetComponentInChildren<Armor>();
        weapon = player.GetComponentInChildren<HeadGun>();
        this.text = this.GetComponent<UnityEngine.UI.Text>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.text.text = string.Format(
@"Tank speed: {1} mph{0}Defensive armor: {2}%{0}Weapon power: {3}{0}Reload time: {4} sec{0}",
             System.Environment.NewLine,
             tankTransform.speed, armor.value * 100,
             weapon.damage, weapon.reloadTime);
    }
}
