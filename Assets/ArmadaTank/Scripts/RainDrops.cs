using UnityEngine;
using System.Collections;

public class RainDrops : MonoBehaviour
{

    public float dropSpeed = 10;
    private float originalY;
    private System.Collections.Generic.List<Material> materials;
    // Use this for initialization
    void Start()
    {
        this.originalY = this.transform.position.y;
        materials = new System.Collections.Generic.List<Material>();
        var renderers = this.GetComponentsInChildren<Renderer>();
        foreach (var item in renderers)
        {
            materials.Add(item.material);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var position = this.transform.position;
        position.y -= Time.deltaTime * dropSpeed;
        var c = Color.Lerp(Color.black, Color.white, originalY - position.y / (originalY + 2.5f));

        if (position.y <= -2.5)
        {
            position.y = originalY;
            //this.renderer.material.color
            foreach (var item in materials)
            {
                item.color = Color.black;
            } 
        }
        else
        {
            foreach (var item in materials)
            {
                item.color = c;
            }
        }
        this.transform.position = position;
    }
}
