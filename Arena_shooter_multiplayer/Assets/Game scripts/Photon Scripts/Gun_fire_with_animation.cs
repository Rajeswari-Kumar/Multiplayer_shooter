using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using Photon.Pun;
using System.IO;
[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class Gun_fire_with_animation : MonoBehaviour
{

    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    public int bulletAmmo = 10;
    public InputActionProperty right_trigger;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")][SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")][SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")][SerializeField] private float ejectPower = 150f;
    public XRGrabInteractable grabbable;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Shoot);
        grabbable.activated.AddListener(CasingRelease);

    }

    void Update()
    {
        //If you want a different input, change it here
        if (right_trigger.action.ReadValue<float>() == 1)
        {
            float float_val = right_trigger.action.ReadValue<float>();
            grabbable.activated.AddListener(gunAnimation);
            Debug.Log("Fltval " + float_val);
        }
    }

    void gunAnimation(ActivateEventArgs arg)
    {
        gunAnimator.SetTrigger("Fire");
    }
    //This function creates the bullet behavior
    void Shoot(ActivateEventArgs arg)
    {

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }
        if (bulletAmmo > 0)
        {
            if (muzzleFlashPrefab)
            {
                //Create the muzzle flash
                GameObject tempFlash;
                tempFlash = PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Fire flash"), barrelLocation.position, barrelLocation.rotation);
                //FindObjectOfType<AudioManager_script>().Play("Gun shot");
                //Destroy the muzzle flash effect
                Destroy(tempFlash, destroyTimer);
            }
            // Create a bullet and add force on it in direction of the barrel
            PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Bullet"), barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
            bulletAmmo--;
            Debug.Log("Bullet" + bulletAmmo);

        }
        else if (bulletAmmo < 1)
        {
            //FindObjectOfType<AudioManager_script>().Play("Dry Gun");
        }

    }

    //This function creates a casing at the ejection slot
    void CasingRelease(ActivateEventArgs arg)
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }
        if (bulletAmmo > 0)
        {
            //Create the casing
            GameObject tempCasing;
            tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
            //Add force on casing to push it out
            tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
            //Add torque to make casing spin in random direction
            tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

            //Destroy casing after X seconds
            Destroy(tempCasing, destroyTimer);
        }

    }

}
