using UnityEngine;
using System.Collections;

public class UIClickeds : MonoBehaviour
{


    public RectTransform targetUI;
    public RectTransform myUI;


    public void OnMyButtonClickAA()
    {

        myUI.anchoredPosition = new Vector2(0, 0);
        //left right 800 离锚点
        targetUI.anchoredPosition = new Vector2(800F, 0);
    }


    public void OnTargetButtonClickBB()
    {

        targetUI.anchoredPosition = new Vector2(0, 0);


        myUI.anchoredPosition = new Vector2(800F, 0);
    }

}