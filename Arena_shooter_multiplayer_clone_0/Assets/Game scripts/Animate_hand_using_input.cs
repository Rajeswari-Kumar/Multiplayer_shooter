using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Animate_hand_using_input : MonoBehaviour
{
    public InputActionProperty GrabAnimationAction;
    public InputActionProperty TriggerAnimationAction;
    
    public Animator hand_animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        grip();
        trigger(); 

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
