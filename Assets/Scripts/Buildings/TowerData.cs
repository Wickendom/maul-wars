using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/BuildingData/Tower")]
public class TowerData : BuildingData
{
    [Header("Tower Details")]
    public int towerGridSize = 2;
    public float towerRange = 3;
    public int damage = 10;
    public float attackSpeed = 1;

    [Header("Projectile Details")]
    public GameObject bulletPrefab;
    public float projectileSpeed = 7;
}
