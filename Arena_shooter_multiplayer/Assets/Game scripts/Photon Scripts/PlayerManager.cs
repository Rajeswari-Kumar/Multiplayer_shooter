using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    GameObject controller;
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

    void CreateController()
    {
         //Instantiate controller for players
        controller =  PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs" , "Player"), Vector3.zero,Quaternion.identity,0, new object[] {PV.ViewID});
    }
    public void Die()
    {
        PhotonNetwork.Destroy(controller);

        //respawn
        CreateController();
    }
}
