using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class WeaponA : MonoBehaviour, IWeapon,IItem
{
    [SerializeField] InventoryItemType itemType;
    [SerializeField] float bulletsPerSecond;
    [SerializeField] float bulletSpeed;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] ItemInfo itemInfo;

    private bool isActive = false;
    private Transform referencePoint;

    private bool isSaved = false;
    public float Frequency => 1 / bulletsPerSecond;

    private int usedSlot = -1;
    public int UsedSlot { get => usedSlot; set => usedSlot = value; }

    public void SetReferencePoint(Transform transform)
    {
        referencePoint = transform;
    }

    public void Shoot()
    {
        BaseBullet bullet = BulletPool.Instance.GetObject();
        bullet.transform.position = referencePoint.position;
        bullet.transform.rotation = referencePoint.rotation;
        bullet.RB.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    public void Activate()
    {
        mesh.enabled = true;
    }

    public void Deactivate()
    {
        mesh.enabled = false;
    }

    #region ISlotItem Interface

    public bool IsSaved => IsSaved;
    private Action onUsed;
    public Action OnUsed => onUsed;

    public GameObject GetGameObject => gameObject;

    public InventoryItemType ItemType => itemType;

    public ItemInfo GetItemInfo()
    {
        return itemInfo;
    }

    private Action<GameObject> setWeapon;

    public void GetItem(PlayerController playerController)
    {
        setWeapon = playerController.WeaponController.SetWeapon;
    }

    public void UseItem()
    {
        setWeapon?.Invoke(this.gameObject);
    }

    #endregion
}
