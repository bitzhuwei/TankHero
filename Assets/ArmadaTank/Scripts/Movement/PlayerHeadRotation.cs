using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerHeadRotation : MonoBehaviour
{
    public float rotationSpeed = 20f;//degrees
    private Vector3 lastRotation;
    private BattleFieldStateManager stateManager;
    private AndroidTouchState touchState;

    //public UnityEngine.UI.Text txtInfo;
    // Use this for initialization
    void Start()
    {
        //if(!txtInfo)
        //{
        //    txtInfo = GameObject.FindGameObjectWithTag(Tags.txtInfo).GetComponent<UnityEngine.UI.Text>();
        //}
        this.touchState = this.GetComponentInParent<AndroidTouchState>();
        var obj = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
        if (obj)
        { this.stateManager = obj.GetComponent<BattleFieldStateManager>(); }
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
        //goCenter.color
#if UNITY_ANDROID
        var target = touchState.GetPlayerFireTarget();
        if(target.HasValue)
        {
            TurnToPoint(target.Value);
        }
        //var builder = new System.Text.StringBuilder();
        //builder.AppendLine(string.Format("player head rotation valid: {0}", target.HasValue));
        //builder.AppendLine("touch info:");
        //builder.AppendLine(string.Format("touchCount:{0}", Input.touchCount));
        //builder.AppendLine("i, fingerId, position, rawPosition, deltaPosition, deltaTime, phase, tapCount");
        //for (int i = 0; i < Input.touches.Length; i++)
        //{
        //    var touch = Input.touches[i];
        //    builder.AppendFormat("[{0}]:{1},{2},{3},{4},{5},{6},{7}", i, touch.fingerId, touch.position, touch.rawPosition, touch.deltaPosition, touch.deltaTime, touch.phase, touch.tapCount);
        //    builder.AppendLine();
        //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        //    eventDataCurrentPosition.position = touch.position;
        //    List<RaycastResult> results = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //    foreach (var item in results)
        //    {
        //        builder.Append(item.gameObject.name);
        //        builder.Append("(");
        //        builder.Append(item.gameObject.GetInstanceID());
        //        builder.Append(")");
        //    }
        //    builder.AppendLine(";");
        //}
        //builder.Append(touchState.goUp.name);
        //builder.Append("(");
        //builder.Append(touchState.goUp.GetInstanceID());
        //builder.Append(")");
        //builder.Append(touchState.goDown.name);
        //builder.Append("(");
        //builder.Append(touchState.goDown.GetInstanceID());
        //builder.Append(")");
        //builder.Append(touchState.goLeft.name);
        //builder.Append("(");
        //builder.Append(touchState.goLeft.GetInstanceID());
        //builder.Append(")");
        //builder.Append(touchState.goRight.name);
        //builder.Append("(");
        //builder.Append(touchState.goRight.GetInstanceID());
        //builder.Append(")");
        //builder.Append(touchState.goCenter.name);
        //builder.Append("(");
        //builder.Append(touchState.goCenter.GetInstanceID());
        //builder.Append(")");
        //txtInfo.text = builder.ToString();
#else
        TurnToPoint(input.mousePosition);
#endif
    }

 

    void TurnToPoint(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
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
