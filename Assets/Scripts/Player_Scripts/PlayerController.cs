using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingEntity
{
    [Header("Player Controller properties")]
    [SerializeField] InputController inputController;
    [SerializeField] CharacterMovement characterMovement;
    [SerializeField] Animator animator;
    [SerializeField] WeaponController weaponController;
    [SerializeField] Camera cam;
    [SerializeField] Inventory inventory;

    //[SerializeField] GameObject[] weaponsObj;

    public WeaponController WeaponController => weaponController;
    public Inventory Inventory => inventory;

    IItem[] weaponSlotItem;
    private Vector3 aimDir;
    private Vector3 walkingDir;

    protected override void Awake()
    {
        base.Awake();
        
        inventory.INIT(this);
    }

    private void Start()
    {
        WeaponsSetup();
    }

    private void WeaponsSetup()
    {
        for (int i = 0; i < inventory.InventoryObjects.Length; i++)
        {
            if(inventory.InventoryObjects[i].InventoryItemType == InventoryItemType.WeaponItem
                && inventory.InventoryObjects[i].gameObject != null)
            {
                inventory.InventoryObjects[i].item.UseItem();
                break;
            }
        }
    }



    public bool CheckItemInFront<T>(LayerMask layer,out T component)
    {
        Debug.Log("CheckItem");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f,layer);
        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log($"{colliders[i].gameObject.name} found");
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            T comp = colliders[i].GetComponent<T>();
            if(comp != null)
            {
                component = comp;
                return true;
            }
        }
        component = default;
        return false;
    }

    public override void Death()
    {
        //sum
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 hitPos = hit.point;
            Vector3 currentPos = transform.position;
            
            //test.position = hitPos;
            hitPos.y = 0;
            currentPos.y = 0;
            aimDir =  (hitPos - currentPos).normalized;
            characterMovement.Aim(aimDir);
        }
        bool shouldShoot = inputController.IsShooting && weaponController.HasWeapon;
        if (inputController.IsShooting && weaponController.HasWeapon)
        {
            weaponController.Shoot();
        }
        characterMovement.Move(inputController.MovementVector);

        animator.SetBool("isRunning", inputController.MovementVector.magnitude > 0.5f);
        animator.SetBool("isShooting", shouldShoot);

        walkingDir = inputController.MovementVector.magnitude > 0.5f ? inputController.MovementVector : walkingDir;

        float dir = Vector3.Dot(aimDir, walkingDir);
        animator.SetFloat("RunningSpeed", dir / Mathf.Abs(dir));
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + aimDir * 4);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + walkingDir * 2);
    }

#endif
}
