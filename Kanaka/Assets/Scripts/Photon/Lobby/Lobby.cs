using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using UnityEngine.UI;
using System.IO;
using System;

public class Lobby : MonoBehaviourPunCallbacks
{

    public byte maxPlayersInRoom = 4;
    public byte minPlayersInRoom = 2;

    public int playerCounter;
    public Text PlayerCounter;

    private string MatchInputFilePath;
    private string CharacterSelected;

    private string path2;


    public void Awake()
    {
        path2 = Application.streamingAssetsPath + "/UsersData/MatchInput.json";

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        //Debug.Log("Do something");
        if (!PhotonNetwork.IsConnected)
        {
            //Debug.Log("Do something");
            if (PhotonNetwork.ConnectUsingSettings())
            {
            }
            else
            {
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Ahora estamos conectados al servidor de la region: " +
            PhotonNetwork.CloudRegion);
    }

    public void JoinRandomRoom()
    {
        if(!PhotonNetwork.JoinRandomRoom())
        {
        }

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        if (PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() { MaxPlayers = maxPlayersInRoom}))
        {
        }
        else
        {
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == minPlayersInRoom)
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel("Multiplayer");
        }
    }
    

    public override void OnJoinedRoom()
    {

        int aux = PhotonNetwork.CurrentRoom.Players.Count;
        Debug.LogError(aux +" currentPlayersroom");
        if (aux == 0||aux == 1)
        {
            string text2 = File.ReadAllText(path2);
            if (text2 != null)
            {
                OnlineUser ou = JsonUtility.FromJson<OnlineUser>(text2);
                ou.team = 0;
                File.WriteAllText(path2, JsonUtility.ToJson(ou));
            }
        }
        else
        {
            string text2 = File.ReadAllText(path2);
            if (text2 != null)
            {
                OnlineUser ou = JsonUtility.FromJson<OnlineUser>(text2);
                ou.team = 1;
                File.WriteAllText(path2, JsonUtility.ToJson(ou));
            }
        }
    }

        public void FixedUpdate()
    {
        if(PhotonNetwork.CurrentRoom != null )
        {
            playerCounter = PhotonNetwork.CurrentRoom.PlayerCount;
            if(PlayerCounter!=null)
            PlayerCounter.text = playerCounter + "/" + maxPlayersInRoom;
        }

    }
}
