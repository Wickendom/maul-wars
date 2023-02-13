using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviourPunCallbacks
{
    private PhotonView phView;

    private byte maxPlayersPerRoom = 2;

    //public InputField playerNameInput;

    private bool inGame = false;

    string gameVersion = "0.1";

    bool isConnecting;

    public void Initialise()
    {
        phView = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }

    public void Connect()
    {
        isConnecting = true;

        //if the client is connected to the server already then join a room.
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else// if not, connect to the server
        {
            string playerName = "Debug";
            PhotonNetwork.LocalPlayer.NickName = playerName;

            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No Random Room Was found. Creating room now");

        PhotonNetwork.CreateRoom(null,
            new RoomOptions
            {
                MaxPlayers = maxPlayersPerRoom,
                IsOpen = true,
                IsVisible = true
            });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room");

        Debug.Log("Loading the Game Scene");

        // Debug.Log("Is user the master client " + PhotonNetwork.IsMasterClient);

        //roomStatus.text = "Room Connected waiting for other players";

        //playersInRoomAmount.text = PhotonNetwork.PlayerList.Length.ToString();
        //if(PhotonNetwork.IsMasterClient)
        //isReducingCountdownTimer = true;

        StartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //playersInRoomAmount.text = PhotonNetwork.PlayerList.Length.ToString();
        Debug.Log("new player joined with actor id " + newPlayer.ActorNumber);

        //if(PhotonNetwork.IsMasterClient)
        //phView.RPC("SetRoomCountdownTimer", newPlayer, roomCountdownTimer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //if(!inGame)
        //playersInRoomAmount.text = PhotonNetwork.PlayerList.Length.ToString();
    }

    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        //PhotonNetwork.CurrentRoom.IsOpen = false;
        inGame = true;

        Debug.LogWarning("Load into game scene here once you have a main menu");
        Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
        //NetworkLoadSceneAsync(1);

        //move this once there is a main menu
        TDRoyaleSingleton.Instance.gameManager.Initialise();
    }

    public void NetworkLoadSceneAsync(int sceneID)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LoadLevel(sceneID);
        }
        else
        {
            SceneManager.LoadScene(sceneID);
        }
    }

    public int GetOtherPlayersID(int playerID)
    {
        if(playerID == 1)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public int GetOpponentsPlayerID()
    {
        if(PhotonNetwork.IsConnected)
        {
            if(PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        return 0;
    }

    public string GetPlayerName(int playerID)
    {
        string name = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].ActorNumber == playerID)
            {
                name = PhotonNetwork.PlayerList[i].NickName;
            }
        }
        return name;
    }
}
