using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.GameFoundation;
using UnityEngine.GameFoundation.DataAccessLayers;
using UnityEngine.GameFoundation.DataPersistence;
using UnityEngine.GameFoundation.Promise;

public class InventoryController : MonoBehaviour
{
    PersistenceDataLayer layer;
    // Start is called before the first frame update
    void Start()
    {
        layer = new PersistenceDataLayer(
                new LocalPersistence("FpsSaveData", new JsonDataSerializer()));
        GameFoundation.Initialize(layer, OnGameFoundationInitialized, Debug.LogError);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGameFoundationInitialized()
    {
        // Here we bind our UI refresh method to callbacks on the inventory manager.
        // These callbacks will automatically be invoked anytime an inventory is added, or removed.
        // This prevents us from having to manually invoke RefreshUI every time we perform one of these actions.
        InventoryManager.onInventoryAdded += RefreshUI;
        InventoryManager.onInventoryRemoved += RefreshUI;

        RefreshUI();
    }

    /// <summary>
    /// This will fill out the main text box with information about the main inventory.
    /// </summary>
    /// <param name="inventoryParam">This parameter will not be used, but must exist so the signature is compatible with the inventory callbacks so we can bind it.</param>
    private void RefreshUI(Inventory inventoryParam = null)
    {
        /*var inventories = InventoryManager.GetInventories();

        // Show the total count of inventories
        inventoryCountText.text = "Total Inventories: " + inventories.Length;

        mainText.text = string.Empty;

        // Loop through every inventory within the manager.
        foreach (Inventory inventory in inventories)
        {
            // Display an empty line between inventories
            mainText.text += "\n";

            // Display the main inventory's display name
            mainText.text += "Inventory - " + inventory.displayName + "\n";

            // Loop through every type of item within the inventory and display its name and quantity.
            foreach (InventoryItem inventoryItem in inventory.GetItems())
            {
                // All game items have an associated display name, this includes game items.
                string itemName = inventoryItem.displayName;

                // Every inventory item has an associated quantity. This represents how many units of this item there are within the inventory.
                int quantity = inventoryItem.quantity;

                mainText.text += itemName + ": " + quantity + "\n";
            }
        }*/
    }
}
