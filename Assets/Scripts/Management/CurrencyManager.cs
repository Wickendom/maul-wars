using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public int money = 250;
    public int income = 100;

    private void Start()
    {
        TDRoyaleSingleton.Instance.uiController.UpdateIncomeText(income);
        TDRoyaleSingleton.Instance.uiController.UpdateMoneyText(money);
        InvokeRepeating("IncomeStep", 10, 10);
    }

    private void IncomeStep()
    {
        money += income;
        TDRoyaleSingleton.Instance.uiController.UpdateMoneyText(money);
    }

    public bool CheckMoney(int amount)
    {
        if(money >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddIncome(int amount)
    {
        income += amount;
        TDRoyaleSingleton.Instance.uiController.UpdateIncomeText(income);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        TDRoyaleSingleton.Instance.uiController.UpdateMoneyText(money);
    }

    public bool RemoveMoney(int amount)
    {
        if(CheckMoney(amount))
        {
            money -= amount;
            TDRoyaleSingleton.Instance.uiController.UpdateMoneyText(money);
            return true;
        }
        return false;
    }
}
