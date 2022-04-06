using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T> where T : IPooleable
{
    void CreateObjects(int amount);

    T GetObject();

    void PutObject(T @object);
}
