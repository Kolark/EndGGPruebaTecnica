using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//Weapon that implements both IWeapon and IItem interfaces
public class WeaponB : MonoBehaviour, IWeapon, IItem
{
    [SerializeField] InventoryItemType itemType;
    [SerializeField] float bulletsPerSecond;
    [SerializeField] float bulletSpeed;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] ItemInfo itemInfo;
    [SerializeField] AudioClip gunSound;


    [SerializeField] private bool isSaved = false;
    private Transform referencePoint;

    private Collider col;
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

    public Collider Col
    {
        get
        {
            if (col == null) { col = GetComponent<Collider>(); }
            return col;
        }
    }
    #region ISlotItem interface

    public bool IsSaved => isSaved;
    public ItemInfo GetItemInfo()
    {
        return itemInfo;
    }

    #endregion
    #region IWeapon Interface
    public float Frequency => 1 / bulletsPerSecond;

    public Action OnUsed => throw new NotImplementedException();

    public GameObject GetGameObject => gameObject;

    public InventoryItemType ItemType => itemType;

    public void Activate()
    {
        mesh.enabled = true;
    }

    public void Deactivate()
    {
        mesh.enabled = false;
    }

    public void SetReferencePoint(Transform transform)
    {
        referencePoint = transform;
    }

    int[] degrees = {-15,-10,0,10,15};

    public void Shoot()
    {
        BaseBullet[] baseBullets = new BaseBullet[5];
        for (int i = 0; i < baseBullets.Length; i++)
        {
            baseBullets[i] = BulletPool.Instance.GetObject();
            baseBullets[i].transform.position = referencePoint.position;

            Vector3 eulerRotation = referencePoint.rotation.eulerAngles;
            
            eulerRotation += referencePoint.up * degrees[i];

            baseBullets[i].transform.rotation = Quaternion.Euler(eulerRotation);

            Vector3 dir = baseBullets[i].transform.forward;

            baseBullets[i].RB.AddForce(dir * bulletSpeed, ForceMode.Impulse);
        }
        currentAudioSource?.PlayOneShot(gunSound);
    }

    private Action<GameObject> setWeapon;


    [SerializeField] AudioSource currentAudioSource;

    public void GetItem(PlayerController playerController)
    {
        if(tweenAnim != null)
        {
            tweenAnim.Kill();
        }
        isSaved = true;
        Col.enabled = false;
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
