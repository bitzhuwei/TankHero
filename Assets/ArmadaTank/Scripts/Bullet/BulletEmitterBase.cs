using UnityEngine;
using System.Collections;

public abstract class BulletEmitterBase : MonoBehaviour {

    public AudioSource emittingSound;
    public bool emitting
    {
        get { return _emitting; }
        set
        {
            if (_emitting != value)
            {
                if (!this.foundShootingAnimation)
                {
                    this.shootingAnimation = this.GetComponentInChildren<ThreeDSAnimation>();
                    if (this.shootingAnimation)
                    {
                        this.foundShootingAnimation = true;
                        this.shootingAnimation.isPlaying = value;
                        //Debug.Log("shootingAnimation played.");
                    }
                }
                else
                {
                    if (this.shootingAnimation)
                    { this.shootingAnimation.isPlaying = value; }
                }
                _emitting = value;
            }
        }
    }
    protected bool _emitting;
    protected bool foundShootingAnimation = false;

    public string bulletName;
    public Transform bulletStartPosition;
    public int projectileLevel;
    public float damage;
    public float reloadTime;
    protected float passedReloadTime;
    protected AudioClipManager audioClipManager;
    public ThreeDSAnimation shootingAnimation;


    protected virtual void Awake()
    {
        this.emittingSound = this.GetComponent<AudioSource>();
    }
    // Use this for initialization
    protected virtual void Start()
    {
        var manager = GameObject.FindGameObjectWithTag(Tags.AudioClipManager);
        if (manager)
        {
            this.audioClipManager = manager.GetComponent<AudioClipManager>();
        }
        else
        {
            Debug.LogWarning(string.Format("AudioClipManager is not found, tank cannot play sound."));
        }
    }

    // Update is called once per frame
    protected abstract void Update();
    //{
    //    passedReloadTime += Time.deltaTime;
    //    if (!this.emitting) { return; }
    //    if (passedReloadTime < this.reloadTime) { return; }
    //    EmitBullet();
    //    passedReloadTime = 0;
    //    emittingSound.Play();
    //}

    protected virtual void EmitBullet()
    {
        var bullet = ResourcesManager.Instantiate(
            PrefabFolder.BattleField + @"/" + bulletName, bulletStartPosition.position, bulletStartPosition.rotation);
        var bulletScript = bullet.GetComponent<BulletBase>();
        bulletScript.Set(projectileLevel, damage, this);
    }
}
