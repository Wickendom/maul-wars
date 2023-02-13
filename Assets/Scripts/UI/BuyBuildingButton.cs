using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBuildingButton : MonoBehaviour
{
    [SerializeField]
    private TowerData data;
    
    public void PurchaseBuilding()
    {
        if(TDRoyaleSingleton.Instance.currencyManager.RemoveMoney(data.cost)) TDRoyaleSingleton.Instance.buildingPlacer.BeginPlacingBuilding(data);
    }
}
