using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    float Frequency { get; }
    void SetReferencePoint(Transform transform);
    void Shoot();
    void Activate();
    void Deactivate();
}

