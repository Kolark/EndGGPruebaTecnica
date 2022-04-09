using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Defines the behaviour of how the enemy should move and aim
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMesh;

    private Transform target;

    //target to chase
    public void SetTarget(Transform target)
    {
        this.target = target;
        navMesh.SetDestination(target.position);
        navMesh.isStopped = true;
    }
    
    public void Move()
    {
        navMesh.SetDestination(target.position);
        navMesh.isStopped = false;
    }

    public void StopMoving()
    {
        navMesh.isStopped = true;
    }

    public void Aim(Vector3 aim)
    {
        navMesh.transform.LookAt(navMesh.transform.position + aim);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(navMesh != null)
        {
            for (int i = 0; i < navMesh.path.corners.Length; i++)
            {
                Gizmos.DrawSphere(navMesh.path.corners[i], 0.5f);
            }
        }
    }
#endif
}
