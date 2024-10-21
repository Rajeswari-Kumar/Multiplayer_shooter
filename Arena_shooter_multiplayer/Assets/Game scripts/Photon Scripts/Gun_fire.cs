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
    public Animate_hand_using_input trigger_val;
    Transform fire_point;
    public string Fire_point_name;
    public float force;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        fire_point = GameObject.Find("Fire point")?.transform;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gun_in_hand && PV.IsMine)
        {
            Debug.Log("Fire");
            fire_bullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            gun_in_hand = true;
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
    public void fire_bullet()
    {
        GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Bullet"), fire_point.position , Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(fire_point.forward * force);
    }
}
