using UnityEngine;
using System.Collections;

public class WinInBattleField : MonoBehaviour {

    public GameObject winPanel;
    public int gainedMoney;
    public UnityEngine.UI.Text moneyDisplayer;
    private EnemySender enemySender;
    private BattleFieldStateManager stateManager;


	// Use this for initialization
	void Start () {
        this.enemySender = this.GetComponent<EnemySender>();
        var obj = GameObject.FindGameObjectWithTag(Tags.BattleFieldManager);
        this.stateManager = obj.GetComponent<BattleFieldStateManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.stateManager.state == EBattleFieldState.Win) { return; }

	    if(this.enemySender.AllSent())
        {
            if(enemySender.AllSentEnemyDead())
            {
                this.winPanel.SetActive(true);
                if (this.stateManager)
                {
                    this.stateManager.state = EBattleFieldState.Win; 
                }
                else
                {
                    Debug.LogWarning(string.Format("No BattleFieldStateManager found in this battle field."));
                }
            }
        }

        this.moneyDisplayer.text = string.Format("￥:{0}", gainedMoney);
	}
}
