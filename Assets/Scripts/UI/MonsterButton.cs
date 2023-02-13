using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterButton : MonoBehaviour
{
    private MonsterData data;

    public void SetMonsterData(MonsterData data)
    {
        if(data == null)
        {
            Debug.LogWarning("Monster Button created but no data was given");
            Destroy(gameObject);
        }
        
        this.data = data;
    }

    public void SpawnMonster()
    {
        TDRoyaleSingleton.Instance.monsterSpawner.SpawnMonster(data.id);
    }
}
