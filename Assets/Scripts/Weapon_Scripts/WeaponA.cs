using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

//Weapon that implements both IWeapon and IItem interfaces
public class WeaponA : MonoBehaviour, IWeapon,IItem
{
    [SerializeField] InventoryItemType itemType;
    [SerializeField] float bulletsPerSecond;
    [SerializeField] float bulletSpeed;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] ItemInfo itemInfo;
    [SerializeField] AudioClip gunSound;

    private Collider col;
    private bool isActive = false;
    private Transform referencePoint;

    [SerializeField] private bool isSaved = false;
    public float Frequency => 1 / bulletsPerSecond;
    public Collider Col
    {
        get
        {
            if (col == null) { col = GetComponent<Collider>(); }
            return col;
        }
    }

    private int usedSlot = -1;
    public int UsedSlot { get => usedSlot; set => usedSlot = value; }

    Tween tweenAnim;
    private void Start()
    {
        if (!isSaved)
        {
            tweenAnim = transform.DOLocalRotate(Vector3.up * 180, 2.0f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

        }
    }

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
        currentAudioSource?.PlayOneShot(gunSound);
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
    [SerializeField] AudioSource currentAudioSource;

    public void GetItem(PlayerController playerController)
    {
        if (tweenAnim != null)
        {
            tweenAnim.Kill();
        }
        Col.enabled = false;
        isSaved = true;
        currentAudioSource = playerController.AudioSource;
        Deactivate();
        setWeapon = playerController.WeaponController.SetWeapon;
    }

    public void UseItem()
    {
        setWeapon?.Invoke(this.gameObject);
    }

    #endregion
}
