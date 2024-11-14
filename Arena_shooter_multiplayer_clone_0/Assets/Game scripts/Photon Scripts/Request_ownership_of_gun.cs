using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request_ownership_of_gun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Gun"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
                return; 
            other.GetComponent<PhotonView>().RPC("request_ownership", RpcTarget.All, other.GetComponent<PhotonView>().ViewID);
        }
    }
}
