using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class related to the handling of an IWeapon object, 
//allows access to the Weapon object itself and it's methods
public class WeaponController : MonoBehaviour
{

    [SerializeField] WeaponPosition weaponPositioner;
    private GameObject currentWeapon;

    public bool HasWeapon => currentWeapon != null;

    private float timer = 0;
    private IWeapon weapon;

    public void SetWeapon(GameObject weapon)
    {
        if(currentWeapon != null)
        {
            ClearWeapon();
        }
        currentWeapon = weapon;
        this.weapon = currentWeapon.GetComponent<IWeapon>();
        this.weapon.SetReferencePoint(weaponPositioner.BulletPosition);
        this.weapon.Activate();
        if(this.weapon == null)
        {
            Debug.LogError("Weapon GameObject does not implement IWeapon Interface");
        }
        weaponPositioner.SetWeapon(currentWeapon.transform);
    }

    private void ClearWeapon()
    {
        weaponPositioner.ReturnWeaponToHolder(currentWeapon.transform);
        weapon.Deactivate();
    }

    public void Shoot()
    {

        if(timer > weapon.Frequency)
        {
            weapon.Shoot();
            timer = 0;
        }
    }

    private void Update()
    {

        if (HasWeapon)
        {
            timer += Time.deltaTime;
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
