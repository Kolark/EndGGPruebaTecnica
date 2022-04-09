using UnityEngine;
using System;

//Simple Helping class notifying when it has been reached by a foreign object and inflicted damage
public class DamageableArea : MonoBehaviour, IDamageable
{
    public Action<int> onDamage;

    public void Damage(int amount)
    {
        onDamage?.Invoke(amount);
    }
}
