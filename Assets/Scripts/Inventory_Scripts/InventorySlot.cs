using System;
using UnityEngine;
using UnityEngine.UI;

//Class that saves the information of an UIIventorySlot, Used by inventory Slot Manager
[Serializable]
public class InventorySlot
{
    [SerializeField] UIInventorySlot uiInventorySlot;
    [SerializeField] KeyCode key;
    [SerializeField] bool focusOnPressed;
    [SerializeField] string keyText;

    public Action onPressed;

    public void INIT()
    {
        uiInventorySlot.Button.onClick.AddListener(Pressed);
    }

    public void SetInventorySlot(ItemInfo itemInfo)
    {
        uiInventorySlot.Img.sprite = itemInfo.slotSprite;
        uiInventorySlot.ActivateKeyText(keyText);
    }
    
    public void ClearInventorySlot()
    {
        uiInventorySlot.Img.sprite = null;
        uiInventorySlot.DeActivateKeyText();
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
