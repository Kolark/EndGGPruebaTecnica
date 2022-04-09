using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Class related to the types of inputs used in the game.
public class InputController : MonoBehaviour
{
    public Action onStartShooting; //When Shooting is pressed
    public Action onStopShooting;  //Called After Shooting is no longer being pressed


    private bool isShooting = false;

    //Getters
    public bool IsShooting => isShooting;
    public Vector3 MovementVector => externalInput.GetMovementDir;
    public Vector3 AimDir => externalInput.GetDir;
    [SerializeField] ExternalInput externalInput;


    private void Update()
    {
        if (!isShooting && externalInput.GetFire1 > 0)
        {
            onStartShooting?.Invoke();
            isShooting = true;
        }
        else if(isShooting && externalInput.GetFire1 == 0)
        {
            onStopShooting?.Invoke();
            isShooting = false;
        }
    }
}
