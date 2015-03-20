using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class AndroidTouchState : MonoBehaviour
{
    public TouchPanelTouching goUp { get; set; }
    public TouchPanelTouching goDown { get; set; }
    public TouchPanelTouching goLeft { get; set; }
    public TouchPanelTouching goRight { get; set; }
    public TouchPanelTouching goCenter { get; set; }
    private float lastUpdatedTime;
    private TankToward playerToward;
    private Vector2? playerFireTarget;
    private UnityEngine.UI.Text txtInfo;

    public TankToward GetPlayerTankToward()
    {
        Update();

        return playerToward;
    }

    public Vector2? GetPlayerFireTarget()
    {
        Update();

        return playerFireTarget;
    }
    // Use this for initialization
    void Start()
    {
        //if (!txtInfo)
        //{
        //    txtInfo = GameObject.FindGameObjectWithTag(Tags.txtInfo).GetComponent<UnityEngine.UI.Text>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if (Time.time != this.lastUpdatedTime)
        {
            UpdateTouchState();
            goUp.playerToward = this.playerToward;
            goDown.playerToward = this.playerToward;
            goLeft.playerToward = this.playerToward;
            goRight.playerToward = this.playerToward;
            goCenter.playerToward = this.playerToward;
            var builder = new System.Text.StringBuilder();
            builder.AppendFormat("Time:{0}", Time.time);
            builder.AppendLine();
            builder.Append("Up    : ");
            builder.AppendLine(goUp.ToString());
            builder.Append("Down  : ");
            builder.AppendLine(goDown.ToString());
            builder.Append("Left  : ");
            builder.AppendLine(goLeft.ToString());
            builder.Append("Right : ");
            builder.AppendLine(goRight.ToString());
            builder.Append("Center: ");
            builder.AppendLine(goCenter.ToString());
            //txtInfo.text = builder.ToString();
            this.lastUpdatedTime = Time.time;
        }
#endif
    }

    private void UpdateTouchState()
    {
        //var builder = new System.Text.StringBuilder();
        //builder.AppendLine(string.Format("{0} touchs:", Input.touchCount));
        var playerTowardDone = false;
        var fireTargetDone = false;
        for (int i = 0; i < Input.touchCount; i++)
        {
            //builder.AppendFormat("[{0}]: ", i);
            var touch = Input.touches[i];
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = touch.position;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            //builder.AppendFormat("{0} hits: [", results.Count);
            var directionArea = false;
            foreach (var raycastResult in results)
            {
                var obj = raycastResult.gameObject;
                var name = obj.name;
                //builder.AppendFormat(obj.ToString());
                //builder.Append(", ");
                if (name == "imgUp")
                {
                    if (!playerTowardDone)
                    {
                        this.playerToward = TankToward.Z;
                        playerTowardDone = true;
                        directionArea = true;
                    }
                }
                else if (name == "imgDown")
                {
                    if (!playerTowardDone)
                    {
                        this.playerToward = TankToward.NZ;
                        playerTowardDone = true;
                        directionArea = true;
                    }
                }
                else if (name == "imgLeft")
                {
                    if (!playerTowardDone)
                    {
                        this.playerToward = TankToward.NX;
                        playerTowardDone = true;
                        directionArea = true;
                    }
                }
                else if (name == "imgRight")
                {
                    if (!playerTowardDone)
                    {
                        this.playerToward = TankToward.X;
                        playerTowardDone = true;
                        directionArea = true;
                    }
                }
                else if (name == "imgCenter")
                {
                    directionArea = true;
                }

                //if (playerTowardDone && fireTargetDone)
                //{ break; }
            }
            //builder.AppendLine("]");

            if (!directionArea)
            {
                if (!fireTargetDone)
                {
                    this.playerFireTarget = touch.position;
                    fireTargetDone = true;
                }
            }

            if (playerTowardDone && fireTargetDone)
            { break; }
        }
        if (!playerTowardDone)
        { this.playerToward = TankToward.None; }
        if (!fireTargetDone)
        { this.playerFireTarget = null; }
        //builder.AppendFormat("TankToward: {0}", playerToward);
        //builder.AppendLine();
        //builder.AppendFormat("fireTarget: {0}", playerFireTarget.HasValue ? playerFireTarget.Value : Vector2.zero);
        //builder.AppendLine();
        //txtInfo.text = builder.ToString();
    }
}
