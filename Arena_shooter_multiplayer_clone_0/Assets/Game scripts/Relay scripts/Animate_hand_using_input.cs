using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Animate_hand_using_input : MonoBehaviour
{
    public InputActionProperty GrabAnimationAction;
    public InputActionProperty TriggerAnimationAction;
    public Animator hand_animator;
    PhotonView PV;
    public float grab_value;
    public float Trigger_value;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        grab_value = GrabAnimationAction.action.ReadValue<float>();
        Trigger_value = TriggerAnimationAction.action.ReadValue<float>();
        grip();
        trigger(); 

    }

    public void grip()
    {
        hand_animator.SetFloat("Grip", grab_value);
    }
    public void trigger()
    {
        hand_animator.SetFloat("Trigger", Trigger_value);
    }
}
