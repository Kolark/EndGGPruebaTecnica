using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int itemsAmount;

    IItem[] items;

    private void Awake()
    {
        items = new IItem[itemsAmount];
    }



}
