using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine.InputSystem;

public class Network_Object_Client : NetworkBehaviour
{
    public InputActionProperty GrabAnimationActionLeft;
    public InputActionProperty GrabAnimationActionRight;
    float grab_value_left;
    float grab_value_right;

    private void Start()
    {
        
    }

    private void Update()
    {
        grab_value_left = GrabAnimationActionLeft.action.ReadValue<float>();
        grab_value_right = GrabAnimationActionRight.action.ReadValue<float>();

    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
            {
                if (grab_value_left > 0 || grab_value_right > 0)
                {
                    //Debug.Log("hello" + grab_value_left + " " + grab_value_right);
                    other.transform.position = transform.position;
                }
            }
    }
}
