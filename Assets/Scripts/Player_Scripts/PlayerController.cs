using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LivingEntity
{
    [Header("Player Controller properties")]
    [SerializeField] InputController inputController;
    [SerializeField] CharacterMovement characterMovement;
    [SerializeField] Animator animator;
    [SerializeField] WeaponController weapon;
    [SerializeField] Camera cam;


    private Vector3 aimDir;
    private Vector3 walkingDir;


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
        bool shouldShoot = inputController.IsShooting && weapon.HasWeapon;
        if (inputController.IsShooting && weapon.HasWeapon)
        {
            weapon.Shoot();
        }
        characterMovement.Move(inputController.MovementVector);

        animator.SetBool("isRunning", inputController.MovementVector.magnitude > 0.5f);
        animator.SetBool("isShooting", shouldShoot);

        walkingDir = inputController.MovementVector.magnitude > 0.5f ? inputController.MovementVector : walkingDir;

        float dir = Vector3.Dot(aimDir, walkingDir);
        animator.SetFloat("RunningSpeed", dir / Mathf.Abs(dir));
    }

    public bool CheckIfItem(out IItem item)
    {
        Collider[] colliders  = Physics.OverlapSphere(transform.position, 2f);
        for (int i = 0; i < colliders.Length; i++)
        {
            IItem colItem = colliders[i].GetComponent<IItem>();
            if(colItem != null)
            {
                item = colItem;
                return true;
            }
        }
        item = null;
        return false;
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
