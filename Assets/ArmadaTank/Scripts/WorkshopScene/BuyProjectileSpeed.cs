using UnityEngine;
using System.Collections;

public class BuyProjectileSpeed : MonoBehaviour {

    public ProjectileSpeed projectileScript;
    public WorkshopConfigLoader configLoader;
    public System.Collections.Generic.List<Sprite> sprites;
    private UnityEngine.UI.Image buttonImage;
    private UnityEngine.UI.Text text;

    void Awake()
    {
        if (sprites == null)
        { sprites = new System.Collections.Generic.List<Sprite>(); }
        buttonImage = this.GetComponent<UnityEngine.UI.Image>();
        this.text = this.GetComponentInChildren<UnityEngine.UI.Text>();
    }

    // Use this for initialization
    void Start()
    {
        var index = configLoader.config.boughtProjectileSpeed;
        buttonImage.sprite = sprites[index];
        this.projectileScript.prefab = (ProjectileSpeed.PrefabOption)index;
        if (index + 1 == sprites.Count)
        {
            this.text.text = string.Format("{0}{1}",
                this.name, System.Environment.NewLine);
        }
        else
        {
            this.text.text = string.Format("{0}{1}￥{2}",
               this.name, System.Environment.NewLine,
               this.configLoader.config.projectileSpeedPrices[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buy_Click()
    {
        var index = configLoader.config.boughtProjectileSpeed;
        if (index + 1 == sprites.Count)
        { return; }

        var price = this.configLoader.config.projectileSpeedPrices[index];
        var money = this.configLoader.config.money;
        if (money >= price)
        {
            this.configLoader.config.money -= price;
            this.configLoader.config.boughtProjectileSpeed = index + 1;
            if (index + 2 == sprites.Count)
            {
                this.text.text = string.Format("{0}{1}",
                    this.name, System.Environment.NewLine);
            }
            else
            {
                this.text.text = string.Format("{0}{1}￥{2}",
                   this.name, System.Environment.NewLine,
                   this.configLoader.config.projectileSpeedPrices[index + 1]);
            }

            buttonImage.sprite = sprites[index + 1];
            this.projectileScript.prefab = (ProjectileSpeed.PrefabOption)(index + 1);
        }
    }
}
