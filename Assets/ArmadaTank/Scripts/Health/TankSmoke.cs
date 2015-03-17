using UnityEngine;
using System.Collections;

public class TankSmoke : MonoBehaviour
{

    public TankHealth tankHealthScript;
    private float lastHealth;

    // Use this for initialization
    void Start()
    {
        //lastHealth = tankHealthScript.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastHealth != tankHealthScript.health)
        {
            var lostHealth = (TankHealth.maxHealth - tankHealthScript.health);
            if (lostHealth <= 0)
            {
                this.particleSystem.enableEmission = false;
            }
            else
            {
                this.particleSystem.Play();
                this.particleSystem.enableEmission = true;
                this.particleSystem.emissionRate = 50 * lostHealth / TankHealth.maxHealth;
            }
            lastHealth = tankHealthScript.health;
        }
    }
}
