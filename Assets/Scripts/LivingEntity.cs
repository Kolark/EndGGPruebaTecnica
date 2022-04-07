using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class LivingEntity : MonoBehaviour
{
    [Header("Living Entity Properties")]
    [SerializeField] int startingHealth;
    [SerializeField] DamageableArea damageableArea;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => startingHealth;
    private float currentHealth = 0;
    public Action onDamage;


    protected virtual void Awake()
    {
        currentHealth = startingHealth;
        damageableArea.onDamage += OnDamage;
    }

    protected virtual void OnDamage(int amount)
    {
        currentHealth -= amount;
        onDamage?.Invoke();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    public abstract void Death();

    protected virtual void OnDestroy()
    {
        damageableArea.onDamage -= OnDamage;
    }

}
