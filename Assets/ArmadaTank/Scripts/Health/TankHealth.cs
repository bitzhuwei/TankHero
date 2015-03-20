using UnityEngine;
using System.Collections;

public abstract class TankHealth : MonoBehaviour
{
    public const float maxHealth = 99;
    public float health;
    protected IConfig baseModelConfig;
    protected GameObject deadAnimation;
    public Transform explosion;

    protected virtual void Awake()
    {
        this.health = 99;
    }

    // Use this for initialization
    void Start()
    {
        baseModelConfig = GetBaseModelConfig();
    }

    protected abstract IConfig GetBaseModelConfig();

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && this.deadAnimation == null)
        {
            var position = this.transform.position;
            position.y = 0.2f;
            var rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            deadAnimation = ResourcesManager.Instantiate(
                PrefabFolder.BattleField + @"/" + PrefabName.strTankExpl,
                position, rotation);
            var setMaterials = deadAnimation.GetComponentsInChildren<SetMaterial>(true);
            var mat = baseModelConfig.GetMaterial();
            foreach (var item in setMaterials)
            {
                item.SetMaterialByName(mat);
            }

            Instantiate(explosion, position, rotation);

            Destroy(this.transform.parent.gameObject);
            //Destroy(deadAnimation, 10);
            OnDead();
        }
    }

    protected abstract void OnTriggerEnter(Collider other);

    protected abstract void OnDead();
}
