using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    [SerializeField] Transform gunHolder;
    [SerializeField] Transform leftHandPosition;
    [SerializeField] Transform bulletPosition;
    [SerializeField] Transform weaponsHolder;

    public Transform BulletPosition => bulletPosition;

    public void SetWeapon(Transform weapon)
    {
        weapon.parent = gunHolder;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    public void ReturnWeaponToHolder(Transform weapon)
    {
        weapon.parent = weaponsHolder;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    public void SetWeaponPosition()
    {
        gunHolder.LookAt(leftHandPosition.position);
    }
}
