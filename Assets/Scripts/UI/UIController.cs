using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using Photon.Pun;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI incomeText;
    [SerializeField]
    private GameObject monsterUIButtonPrefab;
    [SerializeField]
    private Transform towerUIPanel;
    [SerializeField]
    private Transform monsterUIPanel;
    [SerializeField]
    private TextMeshProUGUI winningPlayerText;
    [SerializeField]
    private TextMeshProUGUI playersLivesText;
    [SerializeField]
    private TextMeshProUGUI opponentsLivesText;

    private void Start()
    {
        MonsterData[] monsters = new MonsterData[4];

        for (int i = 0; i < 4; i++)
        {
            monsters[i] = TDRoyaleSingleton.Instance.dataLists.monsterDatas[i];
        }

        MonsterTierContainer monsterTier = new MonsterTierContainer(monsters);
        CreateMonsterMenu(monsterTier);
    }

    public void CreateMonsterMenu(MonsterTierContainer monsterTier)
    {
        CreateMonsterUIButtons(monsterTier.monsterDatas);
    }

    private void CreateMonsterUIButtons(MonsterData[] monsters)
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            GameObject monsterButton = Instantiate(monsterUIButtonPrefab, monsterUIPanel);
            monsterButton.GetComponent<MonsterButton>().SetMonsterData(monsters[i]);
        }
    }

    public void UpdateIncomeText(int newIncome)
    {
        incomeText.text = "Income: " + newIncome.ToString();
    }

    public void UpdateMoneyText(int newMoney)
    {
        moneyText.text = newMoney.ToString();
    }

    public void DisplayWinningPlayerText(int playerID)
    {
        string text = "";

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].ActorNumber == playerID)
            {
                text = PhotonNetwork.PlayerList[i].NickName;
            }
        }

        winningPlayerText.text = text;
    }

    public void UpdatePlayerLivesText(int playerID,int newLivesAmount)
    {
        if(playerID == 1)
        {
            playersLivesText.text = newLivesAmount.ToString();
        }
        else
        {
            opponentsLivesText.text = newLivesAmount.ToString();
        }
    }
}
