using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class Tower : Building
{
    [SerializeField]
    private TowerData data;

    [SerializeField]
    private PhotonView phView;
    private bool isActive;
    private bool initialised = false;

    private Transform target;
    private float currentShootTime = 0;

    [PunRPC]
    public void Initialise(int towerID)
    {
        data = TDRoyaleSingleton.Instance.dataLists.GetTowerData(towerID);
        isActive = true;
        phView = GetComponent<PhotonView>();
        InvokeRepeating("CheckEnemiesInRange", 0f, 0.5f);
    }

    private void CheckEnemiesInRange()
    {
        float closestDistance = Mathf.Infinity;
        Monster closestMonster = null;
        if(TDRoyaleSingleton.Instance.monsterSpawner.attackingPlayerMonsters != null)
        {
            foreach (Monster monster in TDRoyaleSingleton.Instance.monsterSpawner.attackingPlayerMonsters)
            {
                if(monster != null)
                {
                    float distance = Vector3.Distance(transform.position, monster.transform.position);
                    if (distance < closestDistance && distance < data.towerRange)
                    {
                        closestDistance = distance;
                        closestMonster = monster;
                    }
                }
                
            }
        }
        

        if (closestMonster != null)
        {
            target = closestMonster.transform;
        }
        else
        {
            target = null;
        }

    }

    void Update()
    {
        if(isActive)
        {
            if (currentShootTime > 0)
            {
                currentShootTime -= Time.deltaTime;
            }
            else
            {
                if (target != null)
                {
                    Shoot();
                    currentShootTime = data.attackSpeed;
                }
            }
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(data.bulletPrefab, transform.position, data.bulletPrefab.transform.rotation);
        projectile.GetComponent<TowerProjectile>().Seek(target,data.projectileSpeed,data.damage);
    }
}
