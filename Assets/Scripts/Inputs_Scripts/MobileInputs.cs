using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines Mobile Inputs from joysticks
public class MobileInputs : ExternalInput
{
    [SerializeField] Joystick movementJoystick;
    [SerializeField] Joystick dirJoystick;

    public override Vector3 GetMovementDir => new Vector3(movementJoystick.Direction.x,0, movementJoystick.Direction.y);
    public override float GetFire1 { get => dirJoystick.Direction.magnitude > 0 ? 1:0; }
    public override Vector3 GetDir => new Vector3(dirJoystick.Direction.x, 0, dirJoystick.Direction.y);

}
