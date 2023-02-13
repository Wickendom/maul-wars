using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLists : MonoBehaviour
{
    public MonsterData[] monsterDatas;
    public TowerData[] towerDatas;

    private void Start()
    {
        for (int i = 0; i < monsterDatas.Length; i++)
        {
            monsterDatas[i].id = i;
        }
        for (int i = 0; i < towerDatas.Length; i++)
        {
            towerDatas[i].id = i;
        }
    }

    public MonsterData GetMonsterData(int monsterID)
    {
        return monsterDatas[monsterID];
    }

    public TowerData GetTowerData(int towerData)
    {
        return towerDatas[towerData];
    }
}
