using UnityEngine;
using System.Collections;

public class BuyDamage : MonoBehaviour {
    public DamageLevel damageLevelScript;
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
        var index = configLoader.config.boughtDamage;
        buttonImage.sprite = sprites[index];
        this.damageLevelScript.prefab = (DamageLevel.PrefabOption)index;
        if (index + 1 == sprites.Count)
        {
            this.text.text = string.Format("{0}{1}",
                this.name, System.Environment.NewLine);
        }
        else
        {
            this.text.text = string.Format("{0}{1}￥{2}",
               this.name, System.Environment.NewLine,
               this.configLoader.config.damagePrices[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buy_Click()
    {
        var index = configLoader.config.boughtDamage;
        if (index + 1 == sprites.Count)
        { return; }

        var price = this.configLoader.config.damagePrices[index];
        var money = this.configLoader.config.money;
        if (money >= price)
        {
            this.configLoader.config.money -= price;
            this.configLoader.config.boughtDamage = index + 1;
            if (index + 2 == sprites.Count)
            {
                this.text.text = string.Format("{0}{1}",
                    this.name, System.Environment.NewLine);
            }
            else
            {
                this.text.text = string.Format("{0}{1}￥{2}",
                   this.name, System.Environment.NewLine,
                   this.configLoader.config.damagePrices[index + 1]);
            }

            buttonImage.sprite = sprites[index + 1];
            this.damageLevelScript.prefab = (DamageLevel.PrefabOption)(index + 1);
        }
    }
}
