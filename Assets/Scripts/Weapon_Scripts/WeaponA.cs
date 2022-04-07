using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponA : MonoBehaviour, IWeapon, IItem
{
    [SerializeField] float bulletsPerSecond;
    [SerializeField] float bulletSpeed;
    [SerializeField] ItemInfo itemInfo;
    [SerializeField] Collider col;

    private bool isSaved = false;
    private Transform referencePoint;
    private Tween tweenAnim;

    private void Awake()
    {
        tweenAnim = transform.DOLocalRotate(Vector3.up*180, 2.0f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

    }

    public float Frequency => 1 / bulletsPerSecond;
   
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

    #region IItem_Interface
    public bool IsSaved => isSaved;

    public GameObject GetGameObj()
    {
        return gameObject;
    }

    public void GetItem(PlayerController player, InventorySlot slot)
    {
        //Should change weapon in player
        slot.SetInventorySlot(itemInfo);
    }

    public ItemInfo GetItemInfo() => itemInfo;


    #endregion

}
