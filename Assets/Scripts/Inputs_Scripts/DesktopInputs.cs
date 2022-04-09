using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines desktop inputs
public class DesktopInputs : ExternalInput
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    public override Vector3 GetMovementDir
    {
        get
        {
            return 
            Input.GetAxisRaw("Horizontal") * Vector3.right +
            Input.GetAxisRaw("Vertical") * Vector3.forward;
        }
    }
    public override float GetFire1 { get => Input.GetAxis("Fire1"); }
    public override Vector3 GetDir
    {
        get
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitPos = hit.point;
                Vector3 currentPos = player.position;

                //test.position = hitPos;
                hitPos.y = 0;
                currentPos.y = 0;
                aimDir = (hitPos - currentPos).normalized;
            }
            return aimDir;
        }
    }

    private Vector3 aimDir = Vector3.forward;



}
