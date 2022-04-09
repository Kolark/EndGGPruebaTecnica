using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//Class implementing the logic for the Key Item, 
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

    Tween tweenAnim;
    private void Start()
    {
        if (!isSaved)
        {
            tweenAnim = transform.DOLocalRotate(Vector3.up * 180, 2.0f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

        }
    }

    public void GetItem(PlayerController playerController)
    {

                if (tweenAnim != null)
        {
            tweenAnim.Kill();
        }
        checkItem = playerController.CheckItemInFront;
        clearItem = playerController.Inventory.RemoveItem;
        GetComponent<AudioSource>().Play();
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