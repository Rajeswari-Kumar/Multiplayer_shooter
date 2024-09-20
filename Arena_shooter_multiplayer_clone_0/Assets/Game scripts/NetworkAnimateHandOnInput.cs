using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
public class NetworkAnimateHandOnInput : NetworkBehaviour
{
    public InputActionProperty GrabAnimationAction;
    public InputActionProperty TriggerAnimationAction;

    public Animator hand_animator;
    void Update()
    {
        if(IsOwner)
        {
            grip();
            trigger();
        }
        
    }

    public void grip()
    {
        float grab_value = GrabAnimationAction.action.ReadValue<float>();
        hand_animator.SetFloat("Grip", grab_value);
    }
    public void trigger()
    {
        float Trigger_value = TriggerAnimationAction.action.ReadValue<float>();
        hand_animator.SetFloat("Trigger", Trigger_value);
    }
}
