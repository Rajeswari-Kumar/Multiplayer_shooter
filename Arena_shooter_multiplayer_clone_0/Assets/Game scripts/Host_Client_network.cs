using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Host_Client_network : NetworkBehaviour
{
    public void create()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void server()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void join()
    {
        NetworkManager.Singleton.StartClient();
    }
}
