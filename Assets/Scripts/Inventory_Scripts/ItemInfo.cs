using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple Scriptable object that holds the info for an item, mainly to be showed in the UI
[CreateAssetMenu(fileName = "ItemInfo",menuName ="ItemInfo",order =0)]
public class ItemInfo : ScriptableObject
{
    public Sprite slotSprite;
}
