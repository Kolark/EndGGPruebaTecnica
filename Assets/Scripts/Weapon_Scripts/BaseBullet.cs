using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Simple behaviour for a bullet that can be pooled
public class BaseBullet : MonoBehaviour, IPooleable
{

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private TriggerHelper trigger;
    [SerializeField] private Collider col;

    [SerializeField] int damage;

    public Rigidbody RB => rigidbody;

    private Tween deactivateTween;

    private void Awake()
    {
        trigger.onTriggerEnter += OnBulletTriggerEnter;
    }

    public void DeActivate()
    {
        col.enabled = false;
        if (deactivateTween != null && !deactivateTween.IsComplete())
        {
            deactivateTween.Kill();
        }
    }

    public void SetActive()
    {
        col.enabled = true;
        DOVirtual.DelayedCall(5, () => { BulletPool.Instance.PutObject(this); });
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        BulletPool.Instance.PutObject(this);
    }

    public void OnBulletTriggerEnter(Collider other)
    {
        IDamageable toDamage = other.GetComponent<IDamageable>();
        if(toDamage != null)
        {
            toDamage.Damage(damage);
            BulletPool.Instance.PutObject(this);
        }
    }

    private void OnDestroy()
    {
        trigger.onTriggerEnter -= OnBulletTriggerEnter;
    }
}
