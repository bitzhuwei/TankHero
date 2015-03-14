using UnityEngine;
using System.Collections;

public abstract class BulletBase : MonoBehaviour {

    static int idCounter;
    public int id;
    private static readonly float basicSpeed = 0.2f;
    private static readonly float[] defaultSpeed = new float[] { 0, 0.01f, 0.02f, 0.03f, 0.05f };
    public virtual void Set(int speedLevel, float damage, BulletEmitterBase emitter)
    {
        this.speed = basicSpeed + defaultSpeed[speedLevel];
        this.damage = damage;
        this.emitter = emitter;
        this.rigid.velocity = this.trans.rotation * Vector3.forward * speed * 50;
        //.AddForce(this.trans.rotation * Vector3.forward * speed * 3000);
    }

    public float speed { get; set; }
    public float damage { get; set; }
    public BulletEmitterBase emitter { get; set; }
    protected Transform trans;
    protected Rigidbody rigid;
    protected static BattleFieldStateManager stateManager;
    protected EBattleFieldState lastState= EBattleFieldState.Running;

    protected virtual void Awake()
    {
        this.id = idCounter++;
        this.trans = this.transform;
        this.rigid = this.rigidbody;

        //var collider = this.GetComponentInChildren<MeshCollider>();
        //collider.convex = true;
    }

	// Use this for initialization
    protected virtual void Start()
    {
        if (!stateManager)
        {
            var obj = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
            stateManager = obj.GetComponent<BattleFieldStateManager>();
        }
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
        if (!stateManager) { return; }
        if (lastState == stateManager.state) { return; }

        if (stateManager.state == EBattleFieldState.GoodGame
            || stateManager.state == EBattleFieldState.Win
            || stateManager.state == EBattleFieldState.Paused)
        {
            this.rigid.velocity = Vector3.zero;
        }
        else if (stateManager.state == EBattleFieldState.Running)
        {
            this.rigid.velocity = this.trans.rotation * Vector3.forward * speed * 50;
        }

        lastState = stateManager.state;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        var bulletScript = other.GetComponentInParent<BulletBase>();
        if (bulletScript == null) { return; }

        var defence = GetDefenceRule(bulletScript);

        if (!defence)
        {
            bulletScript.Destroy(this);
        }
    }

    private bool GetDefenceRule(BulletBase bulletScript)
    {
        return false;
    }

    public virtual void Destroy(MonoBehaviour other)
    {
        Destroy(this.gameObject);
    }
}
