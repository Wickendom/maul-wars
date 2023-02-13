using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Monster Data")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public GameObject prefab;

    public int health;
    public int movespeed;

    public int cost = 10;
    public int incomeIncreaseAmount = 2;

    [HideInInspector]
    public int id = 0;
}
