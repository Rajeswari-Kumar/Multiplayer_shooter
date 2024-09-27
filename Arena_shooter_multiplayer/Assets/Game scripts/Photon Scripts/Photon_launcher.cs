using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Photon_launcher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log("Connecting to a master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to a master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined in a lobby");
    }
    void Update()
    {
        
    }
}
