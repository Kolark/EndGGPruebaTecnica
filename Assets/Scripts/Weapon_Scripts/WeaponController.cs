using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] WeaponPosition weaponPositioner;
    [SerializeField] GameObject currentWeapon;

    public bool HasWeapon => currentWeapon != null;

    private float timer = 0;
    private IWeapon weapon;
    private void Awake()
    {
        if(currentWeapon != null)
        {
            weapon = currentWeapon.GetComponent<IWeapon>();
            SetWeapon(currentWeapon);
        }
    }

    public void SetWeapon(GameObject weapon)
    {
        currentWeapon = weapon;

        this.weapon = currentWeapon.GetComponent<IWeapon>();
        this.weapon.SetReferencePoint(weaponPositioner.BulletPosition);

        if(weapon == null)
        {
            Debug.LogError("Weapon GameObject does not implement IWeapon Interface");
        }
        weaponPositioner.SetWeapon(currentWeapon.transform);
    }


    public void Shoot()
    {
        timer += Time.deltaTime;
        if(timer > weapon.Frequency)
        {
            weapon.Shoot();
            timer = 0;
        }
    }

    private void Update()
    {
        if(HasWeapon)
        {
            weaponPositioner.SetWeaponPosition();
        }
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if(currentWeapon != null)
        {
            if(currentWeapon.GetComponent<IWeapon>() == null)
            {
                Debug.LogError("Current Weapon object does not implement IWeapon Interface");
            }
        }
    }
#endif
}
