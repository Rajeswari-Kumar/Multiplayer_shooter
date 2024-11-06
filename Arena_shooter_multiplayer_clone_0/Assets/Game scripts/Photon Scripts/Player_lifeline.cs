using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class Player_lifeline : MonoBehaviourPun
{
    public int lifeline = 10;
    RoomManager roomManager;
    PhotonView PV;
    Player_movement_ref player_Movement_Ref;
    public TMP_Text player_lifeline;

    // Start is called before the first frame update
    void Start()
    {
        //roomManager = GetComponent<RoomManager>();
        PV = GetComponent<PhotonView>();
        player_lifeline.text = lifeline.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (PV.IsMine)
              return;
            Debug.Log("Player hit");
            //lifeline--;

           FindObjectOfType<Player_movement_ref>().TakeDamage(1);
           player_lifeline.text = lifeline.ToString();
        }
    }
}
