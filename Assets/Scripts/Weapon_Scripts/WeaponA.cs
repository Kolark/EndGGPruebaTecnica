using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponA : MonoBehaviour, IWeapon
{

    [SerializeField] float bulletsPerSecond;
    [SerializeField] float bulletSpeed;

    private Transform referencePoint;

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

}
