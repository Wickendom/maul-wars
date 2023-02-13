using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class MonsterSpawner : MonoBehaviour
{
    public PhotonView phView;

    [HideInInspector]
    public List<Monster> attackingPlayerMonsters, attackingOpponentMonsters;

    public void Initialise()
    {
        phView = GetComponent<PhotonView>(); 
    }

    public void SpawnMonster(int monsterDataID)
    {
        MonsterData data = TDRoyaleSingleton.Instance.dataLists.GetMonsterData(monsterDataID);
        if (TDRoyaleSingleton.Instance.currencyManager.RemoveMoney(data.cost))
        {
            if (attackingPlayerMonsters == null)
            {
                attackingPlayerMonsters = new List<Monster>();
            }

            GameObject monster = PhotonNetwork.Instantiate(data.monsterName, TDRoyaleSingleton.Instance.pathManager.spawnPoint.transform.position, Quaternion.identity);
            Monster monsterScript = monster.GetComponent<Monster>();
            monster.GetComponent<PhotonView>().RPC("Initialise",RpcTarget.All, data.id, TDRoyaleSingleton.Instance.network.GetOpponentsPlayerID());
            //monsterScript.Initialise(data,TDRoyaleSingleton.Instance.network.GetOpponentsPlayerID());
            GetPlayersMonsterList(TDRoyaleSingleton.Instance.network.GetOpponentsPlayerID()).Add(monsterScript);
            TDRoyaleSingleton.Instance.currencyManager.AddIncome(data.incomeIncreaseAmount);
        }
    }

    public List<Monster> GetPlayersMonsterList(int playerID)
    {
        if(playerID == 0)
        {
            return attackingPlayerMonsters;
        }
        else
        {
            return attackingOpponentMonsters;
        }
    }

    public void RemoveMonster(Monster monster)
    {
        attackingPlayerMonsters.Remove(monster);
    }

    [PunRPC]
    public void UpdateMonsterPathing()
    {
        if (attackingPlayerMonsters != null)
        {
            foreach (Monster monster in attackingPlayerMonsters)
            {
                if(monster != null)
                {
                    monster.GetPath();
                }
            }
        }
    }
}
