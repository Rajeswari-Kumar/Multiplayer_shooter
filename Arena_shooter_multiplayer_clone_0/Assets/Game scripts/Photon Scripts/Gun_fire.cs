using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using System.IO;
public class Gun_fire : MonoBehaviour
{
    public bool gun_in_hand = false;
    public PhotonView PV;
    Animate_hand_using_input trigger_val;
    public GameObject fire_point;
    public float force;
    public int Gun_inventory_ = 10;
    public Animator Animator;
    public int photonViewID;
    void Start()
    {
        photonViewID = PV.ViewID;
    }
    void Update()
    {
       
    }
    public void shoot_gun()
    {
        if (gun_in_hand && PV.IsMine)
        {
            Debug.Log("Fire");
            PV.RPC("Shoot", RpcTarget.All, photonViewID);
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
            GetComponent<Rigidbody>().isKinematic = false;
    }
    [PunRPC]
    public void request_ownership(int photonViewID)
    {
        if (PV.ViewID != photonViewID)
        {
            return;
        }
        GetComponent<OwnershipRequestHandler>().RequestOwnership();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            gun_in_hand = true;
            Debug.Log("gun in hand");
        }
        else
        {
            gun_in_hand= false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        gun_in_hand = false;
    }
    [PunRPC]
    public IEnumerator Shoot(int photonViewID)
    {
        if (PV.ViewID != photonViewID)
            yield break;
        if (PV.IsMine)
        {
            PV.RPC("Shoot_RPC", RpcTarget.All,photonViewID);
            yield return new WaitForSeconds(0.5f);
            PV.RPC("PlayFireAnimationRPC", RpcTarget.All, false);
        }
    }

    [PunRPC]
    public IEnumerator Shoot_RPC(int photonViewID)
    {
        if(PV.ViewID != photonViewID)
            yield break;
        if (PV.IsMine)
        {
            GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Bullet"), fire_point.transform.position, Quaternion.identity);
            PV.RPC("PlayFireAnimationRPC", RpcTarget.All, true);
            bullet.GetComponent<Rigidbody>().AddForce(fire_point.transform.forward * force);
            yield return new WaitForSeconds(2);
            PhotonNetwork.Destroy(bullet);
        }
    }
    [PunRPC]
    public void PlayFireAnimationRPC(bool isFiring)
    {
        if(PV.IsMine)
        Animator.SetBool("Fire", isFiring);
    }
}
