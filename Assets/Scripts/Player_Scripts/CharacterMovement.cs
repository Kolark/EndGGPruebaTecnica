using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that specifies how the player should move;
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementVelocity;

    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Transform playerBody;

    private Quaternion lastRot;

    //Called Every frame
    public void Move(Vector3 movementVec)
    {
        rigidbody.velocity = movementVec.normalized * Time.deltaTime * movementVelocity;
    }

    //
    public void Aim(Vector3 aim)
    {
        playerBody.LookAt(playerBody.position + aim);
    }

}

//[SerializeField] float rotateVelocity;
//if (movementVec.magnitude > 0)
//{
//    playerBody.rotation = Quaternion.Lerp(
//        lastRot, 
//        Quaternion.LookRotation(movementVec.normalized, Vector3.up), 
//        Time.deltaTime * rotateVelocity);

//    lastRot = playerBody.rotation;
//}

//playerBody.rotation = Quaternion.LookRotation(playerBody.position + aim, Vector3.up);