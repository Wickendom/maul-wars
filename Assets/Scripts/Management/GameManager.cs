using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public void Initialise()
    {
        InitialSetup();

        if(PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            Debug.Log("Local player is getting the player setup");
            PlayerSetup();
        }
        else
        {
            Debug.Log("Local player is getting the player setup");
            OpponentSetup();
        }
    }

    void InitialSetup()
    {
        TDRoyaleSingleton.Instance.monsterSpawner.Initialise();
        TDRoyaleSingleton.Instance.tileMap.Initialise();
        TDRoyaleSingleton.Instance.pathManager.Initialise();
    }

    void PlayerSetup()
    {

    }

    void OpponentSetup()
    {
        TDRoyaleSingleton.Instance.cameraManager.UseOpponentsCamera();
    }

    public void PlayerHasNoLivesLeft(int playerID)
    {
        GameOver(TDRoyaleSingleton.Instance.network.GetOtherPlayersID(playerID));
    }

    public void GameOver(int winningPlayerID)
    {
        Debug.Log(TDRoyaleSingleton.Instance.network.GetPlayerName(winningPlayerID) + " has won the game");

    }
}
