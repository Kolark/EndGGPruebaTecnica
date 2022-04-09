using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple Accesor to inputs properties
public class ExternalInput : MonoBehaviour
{
    public virtual Vector3 GetMovementDir { get; }
    public virtual float GetFire1 { get; }
    public virtual Vector3 GetDir { get; }

}
