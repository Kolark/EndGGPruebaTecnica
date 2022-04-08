using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class InventorySlot
{
    [SerializeField] UIInventorySlot uiInventorySlot;
    [SerializeField] KeyCode key;
    [SerializeField] bool focusOnPressed;

    public Action onPressed;

    public void INIT()
    {
        uiInventorySlot.Button.onClick.AddListener(Pressed);
    }

    public void SetInventorySlot(ItemInfo itemInfo)
    {
        uiInventorySlot.Img.sprite = itemInfo.slotSprite;
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
            Pressed();
        }
    }

    private void Pressed()
    {
        onPressed?.Invoke();
    }
}
