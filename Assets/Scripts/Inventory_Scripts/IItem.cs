using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    void GetItem(PlayerController player, InventorySlot slot);
    ItemInfo GetItemInfo();
    GameObject GetGameObj();
    bool IsSaved { get; }
}