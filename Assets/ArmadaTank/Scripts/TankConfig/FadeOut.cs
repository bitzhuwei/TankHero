using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public float fadeOutSpeed = 1f;
    private ThreeDSAnimation threeDSAnimation;
    private float passed;
    private float startFadeOutTime;
    private MeshRenderer meshRenderer;
    private Color initialColor;
    private Color destColor;
    // Use this for initialization
    void Start()
    {
        startFadeOutTime = 0;
        this.threeDSAnimation = this.GetComponent<ThreeDSAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFadeOutTime > 0)
        {
            var t = (Time.time - startFadeOutTime) * fadeOutSpeed;
            meshRenderer.material.color = Color.Lerp(initialColor, destColor, t);
            if(t>1)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            passed += Time.deltaTime;
            if (passed >= threeDSAnimation.cycle * 3)
            {
                startFadeOutTime = Time.time;
                meshRenderer = this.GetComponentInChildren<MeshRenderer>();
                initialColor = meshRenderer.material.color;
                destColor = initialColor;
                destColor.a = 0;
            }
        }
    }
}