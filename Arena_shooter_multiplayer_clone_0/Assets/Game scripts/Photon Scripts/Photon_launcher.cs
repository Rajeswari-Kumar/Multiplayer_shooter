using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;
public class Photon_launcher : MonoBehaviourPunCallbacks
{
    public static Photon_launcher Instance;
    public TMP_Text room_name;
    public TMP_Text room_name_display_created;
    //public TMP_Text room_name_display_joined;
    public TMP_Text room_name_to_join;
    public string room_name_joined;
    public TMP_Text join_code;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListPrefab;
    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject PlayerListPrefab;
    public GameObject player_name;
    public Transform player_list;
    [SerializeField] GameObject startGameButton;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //change this to another function if needed
        Menu_Manager.instance.OpenMenu("Loading");
        Debug.Log("Connecting to a master");
        PhotonNetwork.ConnectUsingSettings();
    }

    //-----connecting to master and joining lobby------//
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to a master");
        PhotonNetwork.JoinLobby();
        //This automaticaally syncs the scene loaded and displayed to all the clients that is connected to the master or host
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Menu_Manager.instance.OpenMenu("Loading");
        Menu_Manager.instance.OpenMenu("Header");
        Debug.Log("Joined in a lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(1, 100).ToString("000");
    }
    //---------------------------------------------//


    //----------create room-----------//
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(room_name.text))
            return;
        PhotonNetwork.CreateRoom(room_name.text);
        Menu_Manager.instance.OpenMenu("Loading");
        room_name_display_created.text = room_name.text;
        Debug.Log("Room created");
    }
    //--------------------------------//

    //-----------Joining a room-----------//
    public void Joinroom(RoomInfo info)
    {
        room_name_joined = info.Name;
        PhotonNetwork.JoinRoom(info.Name);
        Debug.Log("Joined as a client");
        Menu_Manager.instance.OpenMenu("Loading");
    }

    public void JoinRoomCode()
    {
        PhotonNetwork.JoinRoom(join_code.text);
        Menu_Manager.instance.OpenMenu("Loading");
    }
    public override void OnJoinedRoom()
    {
        room_name_display_created.text = PhotonNetwork.CurrentRoom.Name;
        Menu_Manager.instance.OpenMenu("Created Room");
        Debug.Log("In the room");
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Count(); i++)
        {
            player_name = Instantiate(PlayerListPrefab, PlayerListContent);
            player_name.GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        //Debug.Log("player count" + players.Count());
       startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public void startGame()
    {
        //Instead of sceneManagement.loadscene() , PhotonNetwork.LoadLevel()
        //is used so that to all the connected players the level is loaded simultaneously 
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        Player[] players = PhotonNetwork.PlayerList;
        PhotonNetwork.LeaveRoom(room_name);
        Menu_Manager.instance.OpenMenu("Loading");
        
        foreach(PlayerListItem player in player_list.GetComponentsInChildren<PlayerListItem>())
        {
            player.Leaveroom();
        }

    }
    public override void OnLeftRoom()
    {
        Menu_Manager.instance.OpenMenu("Loading");
        Menu_Manager.instance.OpenMenu("Created Room");
        Menu_Manager.instance.OpenMenu("Header");
        Debug.Log("Left room");    }
    //----------------------------------------------//


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
           //Delete room from room list if host leaves the room and no one is in the room
           if (roomList[i].RemovedFromList)
                continue;
           GameObject ROOM = Instantiate(roomListPrefab, roomListContent);
           ROOM.GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
