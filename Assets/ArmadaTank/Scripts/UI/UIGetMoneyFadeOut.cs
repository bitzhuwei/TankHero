using UnityEngine;
using System.Collections;

public class UIGetMoneyFadeOut : MonoBehaviour
{

    public float fadeOutSpeed = 1.5f;
    public float moveUpSpeed = 2;
    private UnityEngine.UI.Image image;
    private RectTransform trans;
    private static readonly Color transparentColor = new Color(1, 1, 1, 0);
    private float startTime;

    // Use this for initialization
    void Start()
    {
        this.image = this.GetComponent<UnityEngine.UI.Image>();
        this.trans = this.GetComponent<RectTransform>();
        this.startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var t = (Time.time - startTime) * fadeOutSpeed;
        if (t < 1)
        {
            this.image.color = Color.Lerp(Color.white, transparentColor, t);
            var position = this.trans.position;
            position.y += moveUpSpeed + Time.deltaTime;
            this.trans.position = position;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
