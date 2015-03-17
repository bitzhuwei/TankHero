using UnityEngine;
using System.Collections;

public class TouchPanelTouching : MonoBehaviour
{
    public UnityEngine.UI.Image leftImage;
    public UnityEngine.UI.Image rightImage;
    public TankToward representingToward;
    public TankToward playerToward { get; set; }
    private TankToward lastPlayerToward;
    private Color touchedColor = new Color(1, 1, 1, 0.5f);
    private static readonly Color untouchedColor = new Color(1, 1, 1, 0f);

    // Use this for initialization
    void Start()
    {
        if (this.leftImage)
        { touchedColor = this.leftImage.color; }
        if (this.leftImage && this.rightImage)
        {
            if (playerToward != representingToward)
            {
                this.leftImage.color = untouchedColor;
                this.rightImage.color = untouchedColor;
            }
            else
            {
                this.leftImage.color = touchedColor;
                this.rightImage.color = touchedColor;
            }
            this.lastPlayerToward = playerToward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.lastPlayerToward == playerToward) { return; }

        if (this.leftImage && this.rightImage)
        {
            if (playerToward != representingToward)
            {
                this.leftImage.color = untouchedColor;
                this.rightImage.color = untouchedColor;
            }
            else
            {
                this.leftImage.color = touchedColor;
                this.rightImage.color = touchedColor;
            }
            this.lastPlayerToward = playerToward;
        }
    }

    public override string ToString()
    {
        if (this.leftImage && this.rightImage)
        { return string.Format("{0}, {1}", this.leftImage.color, this.rightImage.color); }
        else
        { return string.Format("{0}, {1}", this.leftImage, this.rightImage); }
        //return base.ToString();
    }
}
