using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigReference : MonoBehaviour
{
    public static VRRigReference instance;
    public Transform root;
    public Transform head;
    public Transform lefthand;
    public Transform righthand;

    private void Awake()
    {
        instance = this;
    }
}
