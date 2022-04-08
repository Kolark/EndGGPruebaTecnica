using UnityEngine;
using System;
public class InventorySlotManager : MonoBehaviour
{
    [SerializeField] InventorySlot[] slots;

    private static InventorySlotManager instance;
    public static InventorySlotManager Instance => instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

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

    public void SetInventoryItemToSlot(int slot,ItemInfo itemInfo)
    {
        slots[slot].SetInventorySlot(itemInfo);
    }
    
    public void RemoveInventoryItemFromSlot(int slot)
    {
        slots[slot].ClearInventorySlot();
    }

    public void AddListenerToSlot(int slot, Action onPressed)
    {
        slots[slot].onPressed += onPressed;
    }

    public void ClearSlotListener(int slot, Action onPressed)
    {
        slots[slot].onPressed -= onPressed;
    }

}
