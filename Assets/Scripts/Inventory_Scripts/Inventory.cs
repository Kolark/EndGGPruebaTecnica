using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible of handling the player inventory
public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryItem[] inventoryObjects;
    [SerializeField] TriggerHelper triggerHelper;
    [SerializeField] Transform itemsHolder;

    public InventoryItem[] InventoryObjects => inventoryObjects;
    private PlayerController player;

    public void INIT(PlayerController playerController)
    {
        player = playerController;
        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            inventoryObjects[i].INIT();
            if(inventoryObjects[i].gameObject != null)
            {
                TryGetItem(inventoryObjects[i].item);
            }
        }
        triggerHelper.onTriggerEnter += OnItemTriggerEnter;
    }

    public void OnItemTriggerEnter(Collider col)
    {
        IItem item = col.GetComponent<IItem>();
        if (item != null)
        {
            TryGetItem(item); 
        }
    }
    
    private void TryGetItem(IItem item)
    {
        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            if(!inventoryObjects[i].IsFilled && inventoryObjects[i].InventoryItemType == item.ItemType)
            {
                inventoryObjects[i].SetObject(item);
                item.GetItem(player);
                item.UsedSlot = i;
                item.GetGameObject.transform.parent = itemsHolder;
                item.GetGameObject.transform.localPosition = Vector3.zero;
                item.GetGameObject.transform.localRotation = Quaternion.identity;

                InventorySlotManager.Instance.SetInventoryItemToSlot(i, item.GetItemInfo());
                InventorySlotManager.Instance.AddListenerToSlot(i, item.UseItem);
                break;
            }
        }
    }

    public void RemoveItem(int index)
    {
        InventorySlotManager.Instance.RemoveInventoryItemFromSlot(index);
        InventorySlotManager.Instance.ClearSlotListener(index, inventoryObjects[index].item.UseItem);
        inventoryObjects[index].Clear();
    }

    public void OnDestroy()
    {
        triggerHelper.onTriggerEnter -= OnItemTriggerEnter;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            inventoryObjects[i].GetItem();
        }
    }
#endif
}
//Class that tracks a single slot information
[System.Serializable]
public class InventoryItem
{
    public GameObject gameObject;
    public InventoryItemType InventoryItemType;
    public IItem item;

    private bool isFilled = false;

    public bool IsFilled => isFilled;

    public void INIT()
    {
        item = GetItem();
    }

    public void SetObject(IItem item)
    {
        this.gameObject = item.GetGameObject;
        this.item = item;
        isFilled = true;
    }

    public void Clear()
    {
        gameObject = null;
        item = null;
        isFilled = false;
    }

    public IItem GetItem()
    {
        if (gameObject != null)
        {
            IItem itemInterface = gameObject.GetComponent<IItem>();
            if(itemInterface == null)
            {
                gameObject = null;
                Debug.LogError($"{gameObject.name} Does not implement the IItem interface");
            }

            return itemInterface;
        }

        return null;
    }
}
//Types of items to be saved in an inventory slot
public enum InventoryItemType
{
    None,
    WeaponItem,
    UsableItem
}