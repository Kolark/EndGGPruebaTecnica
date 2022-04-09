using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class defining the Enemy's Behaviour
public class EnemyController : LivingEntity
{
    
    [Header("Enemy Controller properties")]
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] Animator enemyAnim;
    [SerializeField] Transform target;
    [SerializeField] WeaponController weaponController;

    //Parameters upon which the enemy can define it's current state
    [Header("Enemy Controller Parameters")]
    [SerializeField] float chasingDistance;
    [SerializeField] float maxShootingDistance;
    [SerializeField] float minShootingDistance;
    [SerializeField] float onTryChaseDuration;

    //Default weapon for the enemy
    [SerializeField] GameObject weapon;

    private EnemyStates currentStatus = EnemyStates.Idle;

    private float chasingTimer = 0;
    private float distanceToTarget = 0;

    //Idle
    //Chasing //Player in View
    //Chasing and Shooting  //Close enough to Shoot
    //Shooting              //Even Closer 

    protected override void Awake()
    {
        base.Awake();
        
        target = FindObjectOfType<PlayerController>().transform.parent;//only demo
        enemyMovement.SetTarget(target);
        weaponController.SetWeapon(weapon);
    }

    public override void Death()
    {
        Destroy(this.transform.parent.gameObject);
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if(currentStatus== EnemyStates.Idle)
        {
            IdleState();
        }
        else if(currentStatus == EnemyStates.Chasing)
        {
            ChasingState();
        }
        else if(currentStatus == EnemyStates.Chasing_Shooting)
        {
            ChasingAndShootingState();
        }
        else if(currentStatus == EnemyStates.Shooting)
        {
            ShootingState();
        }
    }
    #region StateMethods
    private void IdleState()
    {
        currentStatus = distanceToTarget < chasingDistance ? EnemyStates.Chasing : EnemyStates.Idle;

        enemyAnim.SetBool("isRunning", false);
        enemyAnim.SetBool("isShooting", false);

        enemyMovement.StopMoving();
    }

    private void ChasingState()
    {
        Chasing();

        bool isShootingAndChasing = distanceToTarget < maxShootingDistance;

        currentStatus = isShootingAndChasing ? EnemyStates.Chasing_Shooting : EnemyStates.Chasing;
        if (distanceToTarget > chasingDistance)
        {
            chasingTimer += Time.deltaTime;
            if (chasingTimer > onTryChaseDuration)
            {
                chasingTimer = 0;
                currentStatus = EnemyStates.Idle;
            }
        }
        else
        {
            chasingTimer = 0;
        }
    }

    private void ChasingAndShootingState()
    {

        Chasing();
        Shooting();

        bool minShooting = distanceToTarget < minShootingDistance;
        bool chasing = distanceToTarget > maxShootingDistance;

        currentStatus =
            minShooting ? EnemyStates.Shooting :
            chasing ? EnemyStates.Chasing :
            EnemyStates.Chasing_Shooting;
    }

    private void ShootingState()
    {
        Shooting();

        Vector3 dif = target.position - transform.position;
        dif.y = 0;
        dif = dif.normalized;

        enemyMovement.StopMoving();
        enemyMovement.Aim(dif);

        enemyAnim.SetBool("isRunning", false);

        bool isChasingAndShooting = distanceToTarget > minShootingDistance;
        currentStatus = isChasingAndShooting ? EnemyStates.Chasing_Shooting : EnemyStates.Shooting;
    }
    #endregion

    private void Chasing()
    {
        enemyMovement.Move();
        enemyAnim.SetBool("isRunning", true); 
    }

    private void Shooting()
    {
        enemyAnim.SetBool("isShooting", true);
        weaponController.Shoot();
    }



#if UNITY_EDITOR

    private void OnValidate()
    {
        maxShootingDistance = maxShootingDistance > minShootingDistance ? maxShootingDistance : minShootingDistance;
        chasingDistance = chasingDistance > maxShootingDistance ? chasingDistance : maxShootingDistance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chasingDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxShootingDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minShootingDistance);
    }



#endif
}

//All possible enemyStates
public enum EnemyStates
{
    Idle,
    Chasing,
    Chasing_Shooting,
    Shooting,
}
