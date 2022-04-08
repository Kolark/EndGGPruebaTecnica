using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponB : MonoBehaviour, IWeapon, IItem
{
    [SerializeField] InventoryItemType itemType;
    [SerializeField] float bulletsPerSecond;
    [SerializeField] float bulletSpeed;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] ItemInfo itemInfo;

    private bool isSaved = false;
    private Transform referencePoint;

    private Collider col;
    private int usedSlot = -1;
    public int UsedSlot { get => usedSlot; set => usedSlot = value; }

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

    }

    private Action<GameObject> setWeapon;

    public void GetItem(PlayerController playerController)
    {
        Col.enabled = false;
        Deactivate();
        setWeapon = playerController.WeaponController.SetWeapon;
    }

    public void UseItem()
    {
        setWeapon?.Invoke(this.gameObject);
    }


    #endregion
}
