using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using System.IO;
public class Gun_fire : MonoBehaviour
{
    public bool gun_in_hand = false;
    PhotonView PV;
    Animate_hand_using_input trigger_val;
    public GameObject fire_point;
    public float force;
    public int Gun_inventory_ = 10;
    public Animator Animator;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gun_in_hand)
        {
            Debug.Log("Fire");
            StartCoroutine("Shoot");
            GetComponent<OwnershipRequestHandler>().RequestOwnership();
        }
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


    public IEnumerator Shoot()
    {
            Animator.SetBool("Fire", true);
            GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Bullet"), fire_point.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(fire_point.transform.forward * force);
            yield return new WaitForSeconds(0.5f);
            Animator.SetBool("Fire", false);
            yield return new WaitForSeconds(1);
            PhotonNetwork.Destroy(bullet);
    }
}
