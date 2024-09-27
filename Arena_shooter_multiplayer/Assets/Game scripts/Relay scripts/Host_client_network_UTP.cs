using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Host_client_network_UTP : MonoBehaviour
{
    public void Create()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void Server()
    {
        NetworkManager.Singleton.StartServer();
    }
    public void Join()
    {
        NetworkManager.Singleton.StartClient();
    }
}
