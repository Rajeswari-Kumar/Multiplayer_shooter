using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class OwnershipRequestHandler : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    private void Start()
    {
        // Register this script to receive ownership callbacks
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy()
    {
        // Unregister this script to avoid memory leaks
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void OnMouseDown()
    {
        RequestOwnership();
    }

    public void RequestOwnership()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("Already owning the PhotonView.");
            return;
        }
        base.photonView.RequestOwnership();
    }
    // Called when a client requests ownership of a PhotonView
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        // Only the master client should handle ownership requests
        if (PhotonNetwork.IsMasterClient)
        {
            // Approve the ownership request by transferring ownership to the requesting player
            targetView.TransferOwnership(requestingPlayer);
            Debug.Log($"Ownership of {targetView.ViewID} granted to {requestingPlayer.NickName}");
        }
        else
        {
            Debug.LogWarning("Only the master client can approve ownership requests.");
        }
    }

    // Called when ownership is successfully transferred
    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log($"Ownership of {targetView.ViewID} transferred from {previousOwner.NickName} to {targetView.Owner.NickName}");
    }

    // Called if an ownership transfer fails (optional implementation)
    public void OnOwnershipTransferFailed(PhotonView targetView, Player sender)
    {
        Debug.LogError($"Ownership transfer for {targetView.ViewID} failed.");
    }
}
