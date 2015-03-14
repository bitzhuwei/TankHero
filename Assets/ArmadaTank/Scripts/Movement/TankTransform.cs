using UnityEngine;
using System.Collections;

public abstract class TankTransform : MonoBehaviour
{
    protected TankTranslate tankTranslateScript;
    private TankBaseRotation tankBaseRotationScript;
    public TankToward targetMovementVector;
    public TankToward tankBaseToward;
    public float speed;
    protected const float baseSpeed = 0.83f;

    void Awake()
    {
        this.speed = baseSpeed;
    }
    // Use this for initialization
    public virtual void Start()
    {
        tankTranslateScript = this.GetComponent<TankTranslate>();
        tankBaseRotationScript = this.GetComponentInChildren<TankBaseRotation>();
        targetMovementVector = TankToward.None;
        tankBaseToward = TankToward.Z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tankBaseRotationScript.IsRotationCompleted) { return; }
        if (!tankTranslateScript.IsMovementCompleted) { return; }

        this.targetMovementVector = GetNextMovementToward();
    }

    public abstract TankToward GetNextMovementToward();

    public void AddSpeed(float deltaSpeed)
    {
        this.speed = baseSpeed + deltaSpeed;
    }
}
