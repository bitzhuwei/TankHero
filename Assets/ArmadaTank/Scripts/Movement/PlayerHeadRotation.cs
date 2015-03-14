using UnityEngine;
using System.Collections;

public class PlayerHeadRotation : MonoBehaviour
{

    public float rotationSpeed = 20f;//degrees
    private Vector3 lastRotation;
    private BattleFieldStateManager stateManager;


    // Use this for initialization
    void Start()
    {
        var obj = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
        this.stateManager = obj.GetComponent<BattleFieldStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.stateManager)
        {
            if (this.stateManager.state == EBattleFieldState.GoodGame
              || this.stateManager.state == EBattleFieldState.Paused
              || this.stateManager.state == EBattleFieldState.Win)
            { return; }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var v = hit.point - this.transform.position;
            v.y = 0;
            if (v == Vector3.zero || v == this.lastRotation) 
            { return; }

            this.lastRotation = v;
            var toRotation = Quaternion.LookRotation(v, Vector3.up);
            this.transform.rotation = toRotation;
        }
    }
}
