using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementVelocity;
    [SerializeField] float rotateVelocity;
    [SerializeField] Camera cam;
    [SerializeField] Transform test;
    [SerializeField] Transform player;
    [SerializeField] Animator animator;
    //Components

    [SerializeField] Transform gun;
    [SerializeField] Transform leftHand;

    private Rigidbody rigidbody;

    Vector3 hitPos = Vector3.zero;
    Quaternion lastRot;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 movement = Input.GetAxisRaw("Horizontal")*Vector3.right + Input.GetAxisRaw("Vertical")*Vector3.forward;
        rigidbody.velocity = movement.normalized * Time.deltaTime * movementVelocity;
        //rigidbody.AddForce(movement.normalized * Time.deltaTime * movementVelocity,ForceMode.Impulse);
        lastRot = player.rotation;
        if(movement.magnitude > 0)
        {
            player.rotation = Quaternion.Lerp(lastRot, Quaternion.LookRotation(movement.normalized, Vector3.up),Time.deltaTime* rotateVelocity);
            lastRot = player.rotation;
        }
        if(movement.magnitude > 0.5f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if(Input.GetAxis("Fire1") > 0)
        {

            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }

        gun.LookAt(leftHand.position);


    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hitPos, 1);
    }
}


//Ray ray = cam.ScreenPointToRay(Input.mousePosition);
//        if(Physics.Raycast(ray,out RaycastHit hit))
//        {
//            hitPos = hit.point;
//            test.position = hit.point;
//            Vector3 lookAtPos = hit.point;
//lookAtPos.y = transform.position.y;
//            player.LookAt(lookAtPos);
            
//        }