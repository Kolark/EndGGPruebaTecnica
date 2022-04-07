using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventorySlot
{
    [SerializeField] UIInventorySlot uiInventorySlot;
    [SerializeField] KeyCode key;
    [SerializeField] bool focusOnPressed;
    [SerializeField] ItemType itemType;

    public ItemType ItemType => itemType;
    public Action onPressed;

    public void INIT()
    {

    }

    public void SetInventorySlot(ItemInfo itemInfo)
    {
        uiInventorySlot.Img.sprite = itemInfo.slotSprite;
        itemType = itemInfo.itemType;
    }
    
    public void ClearInventorySlot()
    {
        uiInventorySlot.Img.sprite = null;
    }

    //if unity desktop
    public void Update()
    {
        if (Input.GetKeyDown(key))
        {
            onPressed?.Invoke();
        }
    }
}
