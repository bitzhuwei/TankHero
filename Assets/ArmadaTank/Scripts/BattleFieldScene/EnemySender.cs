using UnityEngine;
using System.Collections;

public class EnemySender : MonoBehaviour
{
    public System.Collections.Generic.List<GameObject> sentEnemyTanks;
    public System.Collections.Generic.List<GameObject> sentFriendTanks;
    public Transform mapLoaderObj;
    public float interval = 5;
    private OriginalMapLoader mapLoader;
    private int nextTank = 0;
    private int nextRespawn = 0;
    private float passedInterval;

    // Use this for initialization
    void Start()
    {
        this.mapLoader = mapLoaderObj.GetComponent<OriginalMapLoader>();
        passedInterval = interval;
        if (sentEnemyTanks == null)
        {
            sentEnemyTanks = new System.Collections.Generic.List<GameObject>(mapLoader.map.signedTankCount);
        }
        if(sentFriendTanks==null)
        {
            sentFriendTanks = new System.Collections.Generic.List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nextTank < mapLoader.map.signedTankCount)
        {
            passedInterval += Time.deltaTime;
            if (passedInterval >= interval)
            {
                var tankCode = mapLoader.map.tankList[nextTank++];
                if (tankCode[0] == '0')
                {
                    var tank = SendFriendTank();
                    sentFriendTanks.Add(tank);
                }
                else
                {
                    var enemyObj = ResourcesManager.Instantiate(PrefabFolder.BattleField + @"/" + "Enemy");
                    var enemyModel = enemyObj.GetComponentInChildren<EnemyModel>();
                    enemyModel.SetTankCode(tankCode);
                    enemyObj.transform.position = mapLoader.map.respawnList[nextRespawn++].position;
                    sentEnemyTanks.Add(enemyObj);
                    if (nextRespawn >= mapLoader.map.respawnList.Count)
                    { nextRespawn = 0; }
                    passedInterval = 0;
                }
            }
        }
    }

    private GameObject SendFriendTank()
    {
        Debug.LogError("private void SendAssitantTank() is not implemented.");
        throw new System.NotImplementedException();
    }


    public bool AllSent()
    {
        return !(nextTank < mapLoader.map.signedTankCount);
    }

    public bool AllSentEnemyDead()
    {
        var result = true;
        for (int i = 0; i < sentEnemyTanks.Count; i++)
        {
            if(sentEnemyTanks[i])
            {
                result = false;
                break;
            }
        }
        return result;
    }
}
