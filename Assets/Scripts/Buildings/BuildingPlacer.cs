using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    private bool placingBuilding = false;
    private TowerData data;
    private GameObject buildingToPlace;
    private Vector2 placingCoord;
    private Vector3 placingPos;

    // Update is called once per frame
    void Update()
    {
        if(placingBuilding && buildingToPlace != null)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    placingPos = TDRoyaleSingleton.Instance.tileMap.GetTileFromTouchPosition().worldPos;
                    buildingToPlace.transform.position = placingPos;
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    Destroy(buildingToPlace);
                    PlaceBuilding(TDRoyaleSingleton.Instance.tileMap.GetTileFromTouchPosition());
                }
            }
        }
    }

    public void BeginPlacingBuilding(TowerData data)
    {
        buildingToPlace = Instantiate(data.buildingPrefab);
        this.data = data;
        placingBuilding = true;
    }

    
    public void PlaceBuilding(Tile tile)
    {
        placingBuilding = false;
        buildingToPlace = null;
        TDRoyaleSingleton.Instance.tileMap.phView.RPC("UpdateTileVariables", RpcTarget.All, (int)tile.coords.x, (int)tile.coords.y, false, false);
        GameObject tower = PhotonNetwork.Instantiate(data.buildingPrefab.name,placingPos,Quaternion.identity);
        tower.GetComponent<PhotonView>().RPC("Initialise", RpcTarget.All,data.id);
        TDRoyaleSingleton.Instance.pathManager.phView.RPC("CreatePath",RpcTarget.All,PhotonNetwork.LocalPlayer.ActorNumber);
        TDRoyaleSingleton.Instance.monsterSpawner.phView.RPC("UpdateMonsterPathing",RpcTarget.All);
    }

    public void CancelPlacingBuilding() 
    {
    
    }
}
