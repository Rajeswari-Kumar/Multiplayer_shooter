using Photon.Pun;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Inventory_view : MonoBehaviour
{
    [SerializeField] InputActionProperty leftControllerUI;
    [SerializeField] InputActionProperty rightControllerUI;
    public GameObject inventory_canvas;
    private bool isOpen = false;
    private bool buttonWasPressed = false;
    PhotonView PV;
    [HideInInspector]
    public GameObject weapon;
    public string weapon_name;
    public GameObject player;
    public Inventory_manager inventory_manager;
    private bool isStoring = false;
    void Start()
    {

    }

    void Update()
    {
        handleUIToggle();


        bool is_store = rightControllerUI.action.IsPressed();
        if (is_store && !isStoring && !leftControllerUI.action.IsPressed())
        {
            StartCoroutine(Store_inventory());
        }
    }
    void handleUIToggle()
    {
        if (leftControllerUI.action != null)
        {
            bool isButtonPressed = leftControllerUI.action.IsPressed();
            if (isButtonPressed && !buttonWasPressed && !rightControllerUI.action.IsPressed())
            {
                isOpen = !isOpen;
                inventory_canvas.SetActive(isOpen);
            }
            buttonWasPressed = isButtonPressed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gun") || other.gameObject.CompareTag("Big gun") || other.gameObject.CompareTag("Vest") || other.gameObject.CompareTag("Bullets") || other.gameObject.CompareTag("Life") || other.gameObject.CompareTag("Money"))
        {
            weapon_name = other.gameObject.tag;
            weapon = other.gameObject;
        }
    }

    IEnumerator Store_inventory()
    {
        isStoring = true;
        if (weapon_name == "Gun" || weapon_name == "Big gun" || weapon_name == "Vest" || weapon_name == "Bullets" || weapon_name == "Life" || weapon_name == "Money")
        {
        inventory_manager.inventory_update(weapon_name);
        weapon.GetComponent<PhotonView>().gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        PhotonNetwork.Destroy(weapon);
        }
        isStoring = false;
    }
}
