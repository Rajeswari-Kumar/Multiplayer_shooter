using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class Player_movement_ref : MonoBehaviour
{
    VRRigReference VRRigReference;
    PhotonView PV;
    public Transform head;
    public Transform lefthand;
    public Transform righthand;
    PlayerManager playerManager;
    public Player_lifeline lifeline;
    public float targetXPosition = 2f;
    public float movementSpeed = 2f;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    void Start()
    {
        VRRigReference = GetComponent<VRRigReference>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            //Only owner or this player can control the movement
            head.transform.position = VRRigReference.instance.head.position;
            lefthand.transform.position = VRRigReference.instance.lefthand.position;
            righthand.transform.position = VRRigReference.instance.righthand.position;

            head.transform.rotation = VRRigReference.instance.head.rotation;
            lefthand.transform.rotation = VRRigReference.instance.lefthand.rotation;
            righthand.transform.rotation = VRRigReference.instance.righthand.rotation;
        }
    }

    public void TakeDamage(int damage)
    {
        PV.RPC("RPC_takeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_takeDamage(int damage)
    {
        if (PV.IsMine)
        {
            return;
        }
        lifeline.lifeline -= damage;
        if (lifeline.lifeline <= 0)
            Die();
        Debug.Log("damage" + damage);
    }

    public void Die()
    {
        //playerManager.Die();
        //playerManager.GetComponent<PhotonView>().RPC("Die", RpcTarget.All);

        lifeline.GetComponent<PhotonView>().RPC("Die", RpcTarget.All);
        //PhotonNetwork.LeaveRoom();
        Menu_Manager.instance.OpenMenu("Loading");
    }
}
