using UnityEngine;
using System;
//Interface that every in game item must implement
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