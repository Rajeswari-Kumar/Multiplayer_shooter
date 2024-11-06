using Photon.Pun;
using UnityEngine;

public class NetworkedObjectPhoton : MonoBehaviourPun, IPunObservable
{
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    PhotonView PV;
    private void Start()
    {
        // Initialize network position and rotation to the current position and rotation
        networkPosition = transform.position;
        networkRotation = transform.rotation;
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        // If this GameObject is controlled locally
        if (!photonView.IsMine)
        {
            // Allow local player to control the movement
            // (Movement logic for the local player goes here)
        }
        else
        {
            // Smoothly update the position and rotation for remote players
            PV.RequestOwnership();
            transform.position = networkPosition;
            transform.rotation = networkRotation;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send the object's position and rotation to other players
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            Debug.Log("sending");

        }
        else
        {
            // Receive the object's position and rotation from the network
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            Debug.Log("receiving");
        }
    }
}
