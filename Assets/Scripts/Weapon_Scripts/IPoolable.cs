using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines methods for a class that can be pooled
public interface IPooleable
{
    void SetActive();
    void DeActivate();
}
