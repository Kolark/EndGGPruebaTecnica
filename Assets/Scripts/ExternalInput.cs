using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalInput : MonoBehaviour
{
    public float GetHorizontal => horizontal;
    public float GetVertical => vertical;
    public float GetFire1 => fire1;

    private float horizontal= 0;
    private float vertical=0;
    private float fire1=0;

}
