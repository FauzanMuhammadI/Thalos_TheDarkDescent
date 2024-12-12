using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text quantityTxt;
        [SerializeField] private Image borderImage;

        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

        private bool empty = true;

        public void HideImage()
        {
            if (itemImage != null)
            {
                itemImage.gameObject.SetActive(false); 
            }
        }

        public void ShowImage()
        {
            if (itemImage != null && itemImage.sprite != null)
            {
                itemImage.gameObject.SetActive(true);
            }
        }


        public void Awake()
        {
            ResetData();
            Deselect();
        }

        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void ResetData()
        {
            if (itemImage != null && itemImage.gameObject != null)
            {
                this.itemImage.sprite = null; 
                this.itemImage.gameObject.SetActive(false); 
            }
            this.quantityTxt.text = ""; 
        }

        public void SetData(Sprite sprite, int quantity)
        {
            if (sprite != null)
            {
                this.itemImage.gameObject.SetActive(true); 
                this.itemImage.sprite = sprite; 
            }
            else
            {
                this.itemImage.gameObject.SetActive(false); 
            }

            if (quantity > 0)
            {
                this.quantityTxt.text = quantity.ToString();
            }
            else
            {
                this.quantityTxt.text = ""; 
            }
        }


        public void Select()
        {
            borderImage.enabled = true;
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Left)
            {
                OnItemClicked?.Invoke(this);
            }
            else if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;

            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }


        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

    }
}
