using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    [SerializeField] Transform gunHolder;
    [SerializeField] Transform leftHandPosition;
    [SerializeField] Transform bulletPosition;

    public Transform BulletPosition => bulletPosition;

    public void SetWeapon(Transform weapon)
    {
        weapon.parent = gunHolder;
    }

    public void SetWeaponPosition()
    {
        gunHolder.LookAt(leftHandPosition.position);
    }
}
