using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotManager : MonoBehaviour
{
    [SerializeField] InventorySlot[] slots;

    private void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].INIT();
        }
    }

    private void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Update();
        }
    }

    public void AddInventoryItemToSlot(InventorySlots slot,ItemInfo itemInfo)
    {
        slots[(int)slot].SetInventorySlot(itemInfo);
    }
    
    public void RemoveInventoryItemFromSlot(InventorySlots slot)
    {
        slots[(int)slot].ClearInventorySlot();
    }

}

public enum InventorySlots
{
    Weapon1,
    Weapon2,
    Key
}
