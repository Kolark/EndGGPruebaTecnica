using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IItem
{
    ItemInfo GetItemInfo();
    bool IsSaved { get; }
    int UsedSlot { get; set; }
    Action OnUsed { get; }
    void GetItem(PlayerController playerController);
    void UseItem();
    GameObject GetGameObject { get; }
    InventoryItemType ItemType { get; }
}