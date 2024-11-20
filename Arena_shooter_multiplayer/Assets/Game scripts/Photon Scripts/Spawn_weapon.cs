using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawn_weapon : MonoBehaviour
{
    public Inventory_manager inventory; 
    public List<GameObject> inventory_items; // List of weapon GameObjects
    private int item_number = -1; // Current weapon index (-1 means no weapon initially active)
    public InputActionProperty weapon_spwn; // Input action for weapon spawning
    private bool ispressed = false;

    void Update()
    {
        bool press = weapon_spwn.action.IsPressed();
        if (press && !ispressed)
        {
            StartCoroutine(spawn_weapon());
        }
    }

    IEnumerator spawn_weapon()
    {
        ispressed = true;

        // Deactivate the currently active weapon if any
        if (item_number >= 0 && item_number < inventory_items.Count)
        {
            inventory_items[item_number].SetActive(false);
        }

        // Increment the weapon index and wrap around if needed
        item_number = (item_number + 1) % inventory_items.Count;

        // Activate the new weapon
        if (inventory.itemCounts[item_number] > 0 )
        inventory_items[item_number].SetActive(true);

        Debug.Log($"Weapon activated: {inventory_items[item_number].name}");

        yield return null;

        // Reset the input flag
        ispressed = false;
    }
}
