using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines the methods a weapon should have
public interface IWeapon
{
    float Frequency { get; }
    void SetReferencePoint(Transform transform);
    void Shoot();
    void Activate();
    void Deactivate();
}

