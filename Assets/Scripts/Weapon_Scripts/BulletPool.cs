using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour, IPool<BaseBullet>
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] BaseBullet bullet;

    private Queue<BaseBullet> pool;

    private static BulletPool instance = null;
    public static BulletPool Instance => instance;

    [SerializeField] int amount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        CreateObjects(amount);
    }

    public void CreateObjects(int amount)
    {
        pool = new Queue<BaseBullet>(amount);
        for (int i = 0; i < amount; i++)
        {
            PutObject(CreateObject());
        }
    }

    public BaseBullet GetObject()
    {
        BaseBullet pooleable = pool.Dequeue();
        pooleable.SetActive();
        pooleable.transform.parent = null;
        return pooleable;
    }

    public void PutObject(BaseBullet @object)
    {
        @object.transform.parent = spawnPosition;
        @object.transform.localPosition = Vector3.zero;
        @object.DeActivate();
        pool.Enqueue(@object);
    }

    private BaseBullet CreateObject()
    {
        return Instantiate(bullet.gameObject, spawnPosition.position, Quaternion.identity).GetComponent<BaseBullet>();
    }
}

