using UnityEngine;
using System.Collections;

public class BuyReloadTime : MonoBehaviour {

    public ReloadTimeLevel reloadTimeLevelScript;
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
        var index = configLoader.config.boughtReloadTime;
        buttonImage.sprite = sprites[index];
        this.reloadTimeLevelScript.prefab = (ReloadTimeLevel.PrefabOption)index;
        if (index + 1 == sprites.Count)
        {
            this.text.text = string.Format("{0}{1}",
                this.name, System.Environment.NewLine);
        }
        else
        {
            this.text.text = string.Format("{0}{1}￥{2}",
               this.name, System.Environment.NewLine,
               this.configLoader.config.reloadTimePrices[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buy_Click()
    {
        var index = configLoader.config.boughtReloadTime;
        if (index + 1 == sprites.Count)
        { return; }

        var price = this.configLoader.config.reloadTimePrices[index];
        var money = this.configLoader.config.money;
        if (money >= price)
        {
            this.configLoader.config.money -= price;
            this.configLoader.config.boughtReloadTime = index + 1;
            if (index + 2 == sprites.Count)
            {
                this.text.text = string.Format("{0}{1}",
                    this.name, System.Environment.NewLine);
            }
            else
            {
                this.text.text = string.Format("{0}{1}￥{2}",
                   this.name, System.Environment.NewLine,
                   this.configLoader.config.reloadTimePrices[index + 1]);
            }

            buttonImage.sprite = sprites[index + 1];
            this.reloadTimeLevelScript.prefab = (ReloadTimeLevel.PrefabOption)(index + 1);
        }
    }
}
