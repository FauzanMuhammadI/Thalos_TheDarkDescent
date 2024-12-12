using Inventory.Model;
using Inventory.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage inventoryUI;
        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private int currentlyDraggedIndex = -1; // Menyimpan indeks slot yang sedang di-drag

        private void Start()
        {
            PrepareUI();
            InitializeInventory();
        }

        private void InitializeInventory()
        {
            inventoryUI.ResetAllItems();
            for (int i = 0; i < initialItems.Count; i++)
            {
                if (!initialItems[i].IsEmpty)
                {
                    inventoryUI.UpdateData(i, initialItems[i].item.ItemImage, initialItems[i].quantity);
                }
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(initialItems.Count);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDraggingStart;
            inventoryUI.OnEndDragging += HandleDraggingEnd;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= initialItems.Count || initialItems[itemIndex].IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }

            InventoryItem item = initialItems[itemIndex];
            inventoryUI.UpdateDescription(itemIndex, item.item.ItemImage, item.item.name, item.item.Description);
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            if (itemIndex1 < 0 || itemIndex1 >= initialItems.Count || itemIndex2 < 0 || itemIndex2 >= initialItems.Count)
                return;

            InventoryItem temp = initialItems[itemIndex1];
            initialItems[itemIndex1] = initialItems[itemIndex2];
            initialItems[itemIndex2] = temp;

            inventoryUI.UpdateData(itemIndex1, initialItems[itemIndex1].item?.ItemImage, initialItems[itemIndex1].quantity);
            inventoryUI.UpdateData(itemIndex2, initialItems[itemIndex2].item?.ItemImage, initialItems[itemIndex2].quantity);

            currentlyDraggedIndex = -1; 
        }

        private void HandleDraggingStart(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= initialItems.Count || initialItems[itemIndex].IsEmpty)
                return;

            currentlyDraggedIndex = itemIndex;

            inventoryUI.HideItemImage(itemIndex);

            InventoryItem item = initialItems[itemIndex];
            inventoryUI.CreateDraggedItem(item.item.ItemImage, item.quantity);
        }



        private void HandleDraggingEnd()
        {
            if (currentlyDraggedIndex < 0 || currentlyDraggedIndex >= initialItems.Count)
                return;

            inventoryUI.ShowItemImage(currentlyDraggedIndex);

            currentlyDraggedIndex = -1;

            inventoryUI.ResetDraggedItem();
        }


        private void HandleItemActionRequest(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= initialItems.Count || initialItems[itemIndex].IsEmpty)
                return;

            InventoryItem inventoryItem = initialItems[itemIndex];
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject);
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                RemoveItem(itemIndex, 1);
            }
        }

        public void AddItem(ItemSO item, int quantity)
        {
            if (item.IsStackable)
            {
                for (int i = 0; i < initialItems.Count; i++)
                {
                    if (initialItems[i].item == item)
                    {
                        initialItems[i] = initialItems[i].ChangeQuantity(initialItems[i].quantity + quantity);
                        inventoryUI.UpdateData(i, initialItems[i].item.ItemImage, initialItems[i].quantity);
                        return;
                    }
                }
            }

            for (int i = 0; i < initialItems.Count; i++)
            {
                if (initialItems[i].IsEmpty)
                {
                    initialItems[i] = new InventoryItem { item = item, quantity = quantity };
                    inventoryUI.UpdateData(i, item.ItemImage, quantity);
                    return;
                }
            }

            Debug.LogWarning("Inventory is full! Cannot add more items.");
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (itemIndex < 0 || itemIndex >= initialItems.Count || initialItems[itemIndex].IsEmpty)
                return;

            int remainingQuantity = initialItems[itemIndex].quantity - amount;
            if (remainingQuantity <= 0)
            {
                initialItems[itemIndex] = InventoryItem.GetEmptyItem();
            }
            else
            {
                initialItems[itemIndex] = initialItems[itemIndex].ChangeQuantity(remainingQuantity);
            }

            inventoryUI.UpdateData(itemIndex, initialItems[itemIndex].item?.ItemImage, initialItems[itemIndex].quantity);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!inventoryUI.isActiveAndEnabled)
                {
                    inventoryUI.Show();
                    for (int i = 0; i < initialItems.Count; i++)
                    {
                        if (!initialItems[i].IsEmpty)
                        {
                            inventoryUI.UpdateData(i, initialItems[i].item.ItemImage, initialItems[i].quantity);
                        }
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
        }
    }
}
