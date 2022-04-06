using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputController inputController;
    [SerializeField] CharacterMovement characterMovement;
    [SerializeField] Animator animator;
    [SerializeField] WeaponController weapon;
    [SerializeField] DamageableArea damageable;

    [SerializeField] Camera cam;

    [SerializeField] int startingHealth;

    private Vector3 aimDir;
    private Vector3 walkingDir;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
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
        if (inputController.IsShooting)
        {
            weapon.Shoot();
        }
        characterMovement.Move(inputController.MovementVector);

        animator.SetBool("isRunning", inputController.MovementVector.magnitude > 0.5f);
        animator.SetBool("isShooting", inputController.IsShooting);

        walkingDir = inputController.MovementVector.magnitude > 0.5f ? inputController.MovementVector : walkingDir;

        float dir = Vector3.Dot(aimDir, walkingDir);
        animator.SetFloat("RunningSpeed", dir / Mathf.Abs(dir));
    }


    private void OnDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            startingHealth = 0;
            //Dead or something idk
        }
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
