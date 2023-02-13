using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public int lives = 20; //this is the amount of times an enemy can reach the end before the player losing
    
    private int playersCurrentLives;
    private int opponentsCurrentLives;

    public void RemoveLife(int playerID)
    {
        if(playerID == 1)
        {
            playersCurrentLives--;
            TDRoyaleSingleton.Instance.uiController.UpdatePlayerLivesText(playerID, playersCurrentLives);
            if(playersCurrentLives <= 0)
            {
                TDRoyaleSingleton.Instance.gameManager.PlayerHasNoLivesLeft(playerID);
            }
        }
        else
        {
            opponentsCurrentLives--;
            TDRoyaleSingleton.Instance.uiController.UpdatePlayerLivesText(playerID, opponentsCurrentLives);
            if (playersCurrentLives <= 0)
            {
                TDRoyaleSingleton.Instance.gameManager.PlayerHasNoLivesLeft(playerID);
            }
        }
    }
}
