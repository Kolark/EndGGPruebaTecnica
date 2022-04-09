using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Class that defines the living Behaviour upon which a player or an enemy can inherit from
public abstract class LivingEntity : MonoBehaviour
{
    [Header("Living Entity Properties")]
    [SerializeField] int startingHealth;
    [SerializeField] DamageableArea damageableArea;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => startingHealth;
    private float currentHealth = 0;
    public Action onLifeChanged;
    public Action onDeath;

    protected virtual void Awake()
    {
        currentHealth = startingHealth;
        damageableArea.onDamage += OnDamage;
    }

    protected virtual void OnDamage(int amount)
    {
        currentHealth -= amount;
        onLifeChanged?.Invoke();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    public abstract void Death();

    protected void Revive()
    {
        currentHealth = 100;
        onLifeChanged?.Invoke();
    }

    protected virtual void OnDestroy()
    {
        damageableArea.onDamage -= OnDamage;
    }

}
