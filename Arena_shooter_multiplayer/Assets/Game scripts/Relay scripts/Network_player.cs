using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Network_player : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform lefthand;
    public Transform righthand;

    public Renderer[] meshToDisable;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsOwner)
        {
            foreach(var item in meshToDisable)
            {
                item.enabled = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(IsOwner)
        {
            root.position = VRRigReference.instance.root.position;
            root.rotation = VRRigReference.instance.root.rotation;

            head.position = VRRigReference.instance.head.position;
            head.rotation = VRRigReference.instance.head.rotation;

            lefthand.position = VRRigReference.instance.lefthand.position;
            lefthand.rotation = VRRigReference.instance.lefthand.localRotation;

            righthand.position = VRRigReference.instance.righthand.position;
            righthand.rotation = VRRigReference.instance.righthand.rotation;
        }
    }
}
