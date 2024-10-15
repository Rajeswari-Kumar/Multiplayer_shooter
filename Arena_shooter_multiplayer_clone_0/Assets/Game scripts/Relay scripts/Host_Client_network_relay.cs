using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Networking.Transport;
using System.Text;
using System;
using UnityEngine.UI;
using System.Linq;
public class Host_Client_network_relay : MonoBehaviour
{
    public int maxCollection = 5;
    public string newJoinCode;
    public Text text;
    public UnityTransport transport;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        //await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    private void Start()
    {
        transport.ConnectTimeoutMS = 30000; 
        transport.DisconnectTimeoutMS = 30000; 
    }
    public async void create()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        try {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxCollection);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "udp"));
            var relayServerData = new RelayServerData(allocation, "udp");
            newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);
            NetworkManager.Singleton.StartHost();
            text.text = newJoinCode;
            var dtlsEndpoint = allocation.ServerEndpoints.First(e => e.ConnectionType == "udp");
            var settings = new NetworkSettings();
            settings.WithRelayParameters(ref relayServerData);
            NetworkDriver hostDriver = NetworkDriver.Create(settings);
        }
        catch (RelayServiceException e)
        {
            Debug.LogError("Relay service error: " + e);
        }
        
    
}

    public async void join()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        if (string.IsNullOrEmpty(newJoinCode))
        {
            Debug.LogError("Join code is empty or null.");
            return;
        }
        try
        {
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(newJoinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "udp"));
            transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData,allocation.HostConnectionData);
            Debug.Log("Client session id " + allocation.AllocationId);
            NetworkManager.Singleton.StartClient();
        }
        catch(RelayServiceException e)
        {
            Debug.Log(e);
        }

    }


    public void server()
    {
        NetworkManager.Singleton.StartServer();
    }
}
