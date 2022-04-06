using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Class related to the types of inputs used in the game.
public class InputController : MonoBehaviour
{
    public Action onStartShooting; //When Shooting is pressed
    public Action onStopShooting;  //Called After Shooting is no longer being pressed
    private Vector3 movementVector;

    private bool isShooting = false;

    //Getters
    public bool IsShooting => isShooting;
    public Vector3 MovementVector => movementVector;

    private void Update()
    {
        movementVector = 
            Input.GetAxisRaw("Horizontal") * Vector3.right + 
            Input.GetAxisRaw("Vertical") * Vector3.forward;
        if (!isShooting && Input.GetAxis("Fire1") > 0)
        {
            onStartShooting?.Invoke();
            isShooting = true;
        }
        else if(isShooting && Input.GetAxis("Fire1") == 0)
        {
            onStopShooting?.Invoke();
            isShooting = false;
        }
    }

}
