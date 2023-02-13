using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class Monster : MonoBehaviour
{
    public MonsterData data;

    [HideInInspector]
    public PhotonView phView;

    public int playerIDToAttack;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private int pathPointIndex = 0;

    private List<Vector3> currentPathPositions;
    private int currentPathIndex;//this is the index of the vector3's in the monsters current path

    bool moving = false;

    [PunRPC]
    public void Initialise(int dataID,int playerIDToAttack)
    {
        phView = GetComponent<PhotonView>();

        MonsterData data = TDRoyaleSingleton.Instance.dataLists.GetMonsterData(dataID);

        this.data = data;
        currentHealth = data.health;
        this.playerIDToAttack = playerIDToAttack;
        
        GetPath();
    }


    public void SetPathPoint(int index)
    {
        if(index > pathPointIndex) pathPointIndex = index;
    }

    private void FixedUpdate()
    {
        if(moving)
        {
            if (Vector3.Distance(transform.position, TDRoyaleSingleton.Instance.pathManager.GetPathPointPosition(pathPointIndex + 1)) < 0.1f)
            {
                pathPointIndex++;
            }

            Vector3 dir = currentPathPositions[currentPathIndex] - transform.position;
            transform.Translate(dir.normalized * data.movespeed * Time.deltaTime, Space.World);
            transform.LookAt(currentPathPositions[currentPathIndex]);
            if (Vector3.Distance(transform.position, currentPathPositions[currentPathIndex]) < 0.1)
            {
                currentPathIndex++;
                if (currentPathIndex == currentPathPositions.Count - 1)
                {
                    Die();
                }
                
            }
        }
    }

    public void TakeDamage(int amount)
    {
        //Debug.Log("Taking " + amount + " Damage");

        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void GetPath()
    {
        currentPathIndex = 1;
        currentPathPositions = TDRoyaleSingleton.Instance.pathManager.CreatePathFromMonsterToPlayersEndPoint(playerIDToAttack,TDRoyaleSingleton.Instance.tileMap.GetTileFromWorldPosition(transform.position), pathPointIndex);
        
        if (!moving) moving = true;
    }

    private void Die()
    {
        TDRoyaleSingleton.Instance.monsterSpawner.RemoveMonster(this);
        Destroy(gameObject);
    }
}
