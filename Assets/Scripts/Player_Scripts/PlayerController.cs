using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that defines the behaviour of the Player Controller
public class PlayerController : LivingEntity
{
    [Header("Player Controller properties")]
    [SerializeField] InputController inputController;
    [SerializeField] CharacterMovement characterMovement;
    [SerializeField] Animator animator;
    [SerializeField] WeaponController weaponController;

    [SerializeField] Inventory inventory;
    [SerializeField] AudioSource audioSource;


    //[SerializeField] GameObject[] weaponsObj;

    public AudioSource AudioSource => audioSource;
    public WeaponController WeaponController => weaponController;
    public Inventory Inventory => inventory;

    IItem[] weaponSlotItem;

    private Vector3 walkingDir;
    private Vector3 playerAimDir = Vector3.right;

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


    //Helping methods that can get a certain component if the object is to be found via
    //the collision of an overlapping sphere
    public bool CheckItemInFront<T>(LayerMask layer,out T component)
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f,layer);

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
        onDeath?.Invoke();
        //only demo
        Revive();
    }

    private void Update()
    {
        Vector3 movementVector = inputController.MovementVector;
        Vector3 aimDir = inputController.AimDir;

        characterMovement.Aim(aimDir);

        bool shouldShoot = inputController.IsShooting && weaponController.HasWeapon;
        if (inputController.IsShooting && weaponController.HasWeapon)
        {
            weaponController.Shoot();
        }

        characterMovement.Move(movementVector);

        animator.SetBool("isRunning", movementVector.magnitude > 0.5f);
        animator.SetBool("isShooting", shouldShoot);

        walkingDir = movementVector.magnitude > 0.5f ? movementVector : walkingDir;
        playerAimDir = aimDir.magnitude > 0.5f ? aimDir : playerAimDir;

        float dir = Vector3.Dot(playerAimDir, walkingDir);
        animator.SetFloat("RunningSpeed", dir / Mathf.Abs(dir));
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + aimDir * 4);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + walkingDir * 2);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }

#endif
}
