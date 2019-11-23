using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{
    public Button connectBtn;
    public Button joinRandomRoomBtn;
    public Text Log;

    public byte maxPlayersInRoom = 4;
    public byte minPlayersInRoom = 2;

    public int playerCounter;
    public Text PlayerCounter;

    public void Awake()
    {
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
                Log.text += "\nEstamos conectados al servidor";
            }
            else
            {
                Log.text += "\nError al conectar al servidor";
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Ahora estamos conectados al servidor de la region: " +
            PhotonNetwork.CloudRegion);
        connectBtn.interactable = false;
        joinRandomRoomBtn.interactable = true;
    }

    public void JoinRandomRoom()
    {
        if(!PhotonNetwork.JoinRandomRoom())
        {
            Log.text += "\nFallo al unirse a la sala";
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Log.text += "\n No existen salas a las que unirse, creando una nueva";

        if (PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() { MaxPlayers = maxPlayersInRoom}))
        {
            Log.text += "\n Sala creada con exito";
        }
        else
        {
            Log.text += "\n fallo al crear la sala";
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == minPlayersInRoom)
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel("Escena Photon Prueba2");
        }
    }
    

    public override void OnJoinedRoom()
    {
        Log.text += "\n Unido a la sala";
        joinRandomRoomBtn.interactable = false;
        
    }

        public void FixedUpdate()
    {
        if(PhotonNetwork.CurrentRoom != null )
        {
            playerCounter = PhotonNetwork.CurrentRoom.PlayerCount;
        }
        PlayerCounter.text = playerCounter + "/" + maxPlayersInRoom;
    }
}
