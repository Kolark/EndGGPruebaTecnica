using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IItem
{
    [SerializeField] InventoryItemType itemType;
    [SerializeField] LayerMask layer;
    [SerializeField] ItemInfo itemInfo;
    [SerializeField] GameObject mesh;
    [SerializeField] Collider col;

    private bool isSaved;
    private Action onUsed;

    public Action OnUsed => onUsed;

    public bool IsSaved => isSaved;

    public GameObject GetGameObject => gameObject;

    public InventoryItemType ItemType => itemType;

    private int usedSlot = -1;
    public int UsedSlot { get => usedSlot; set => usedSlot = value; }

    private CheckItem checkItem;
    private Action<int> clearItem;

    public void GetItem(PlayerController playerController)
    {
        checkItem = playerController.CheckItemInFront;
        clearItem = playerController.Inventory.RemoveItem;
        isSaved = true;
        col.enabled = !isSaved;
        mesh.SetActive(!isSaved);
    }

    public void UseItem()
    {
        Debug.Log("Key being used");
        if(checkItem != null && checkItem(layer, out IUnlockable unlockable))
        {
        Debug.Log("Managed to find something");
            if (unlockable.Unlock())
            {
                clearItem?.Invoke(usedSlot);
                Destroy(this.gameObject);
                clearItem = null;
                isSaved = false;
            }
        }
        onUsed?.Invoke();
    }

    public ItemInfo GetItemInfo()
    {
        return itemInfo;
    }


}
public delegate bool CheckItem(LayerMask layer, out IUnlockable unlockable);