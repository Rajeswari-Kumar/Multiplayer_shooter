using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.Rendering;
using Unity.Netcode;
public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public GameObject controller;
    Transform spawnpoint;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void Update()
    {

    }

    public void CreateController()
    {
         //Instantiate controller for players
        controller =  PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs" , "Player"),new Vector3(1,0,0),Quaternion.identity,0, new object[] {PV.ViewID});
    }
     [PunRPC]
    public void Die()
    {
        if(controller == null)
        {
            Debug.Log("no player");
            return;
        }
        Destroy(controller);
        
        Debug.Log("dead");
        //respawn
        //CreateController();
    }
}
