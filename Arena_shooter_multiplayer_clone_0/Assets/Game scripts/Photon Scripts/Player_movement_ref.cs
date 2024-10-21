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

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
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
}
