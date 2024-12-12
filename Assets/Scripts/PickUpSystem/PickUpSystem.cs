using Inventory;
using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryController;

    private void OnTriggerEnter(Collider collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            inventoryController.AddItem(item.InventoryItem, item.Quantity);
            item.DestroyItem();
        }
    }
}
