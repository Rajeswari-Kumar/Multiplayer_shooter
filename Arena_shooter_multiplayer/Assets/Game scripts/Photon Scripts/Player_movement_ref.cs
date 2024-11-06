using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Player_movement_ref : MonoBehaviour
{
    VRRigReference VRRigReference;
    PhotonView PV;
    public Transform head;
    public Transform lefthand;
    public Transform righthand;
    PlayerManager playerManager;
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

    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_takeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_takeDamage(int damage, int lifeline)
    {
        if (PV.IsMine)
        {
            return;
        }
        lifeline -= damage;
        if (lifeline <= 0)
            Die();
        Debug.Log("damage" + damage);
    }

    void Die()
    {
        playerManager.Die();
    }
}
