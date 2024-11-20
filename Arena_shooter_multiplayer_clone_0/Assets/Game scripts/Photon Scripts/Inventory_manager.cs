using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
public class Inventory_manager : MonoBehaviour
{
    public List<string> inventoryItems = new List<string> { "Gun", "Big gun", "Vest", "Money", "Life", "Bullets", "Big Bullets", "Chest" };
    public List<int> itemCounts = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };
    [SerializeField] private List<TMP_Text> itemTexts;

    void Start()
    {
        UpdateInventoryUI();
    }

    void Update()
    {
    }

    public void inventory_update(string weapon)
    {
        int index = inventoryItems.IndexOf(weapon);
        if (index >= 0)
        {
            itemCounts[index]++;
            UpdateInventoryUI();
        }
        else
        {
            Debug.LogWarning($"Item '{weapon}' not found in inventory.");
        }
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (i < itemTexts.Count)
            {
                itemTexts[i].text = $"{itemCounts[i]}";
            }
        }
    }

}
