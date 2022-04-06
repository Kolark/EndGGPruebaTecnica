using UnityEngine;
using System;

public class DamageableArea : MonoBehaviour, IDamageable
{
    public Action<int> onDamage;

    public void Damage(int amount)
    {
        onDamage?.Invoke(amount);
    }
}
