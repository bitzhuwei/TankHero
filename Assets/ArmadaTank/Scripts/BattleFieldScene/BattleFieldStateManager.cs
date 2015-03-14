using UnityEngine;
using System.Collections;

public class BattleFieldStateManager : MonoBehaviour {

    public EBattleFieldState state;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public enum EBattleFieldState
{
    Running,
    Paused,
    Win,
    GoodGame,
}