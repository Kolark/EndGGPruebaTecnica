using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple poolable interface that every system that uses pooling should implement
public interface IPool<T> where T : IPooleable
{
    void CreateObjects(int amount);

    T GetObject();

    void PutObject(T @object);
}
