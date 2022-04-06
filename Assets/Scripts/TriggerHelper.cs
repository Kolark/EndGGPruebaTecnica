using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerHelper : MonoBehaviour
{
    public Action<Collider> onTriggerExit;
    public Action<Collider> onTriggerEnter;

    private void OnTriggerEnter(Collider col)
    {
        onTriggerEnter?.Invoke(col);
    }

    private void OnTriggerExit(Collider col)
    {
        onTriggerExit?.Invoke(col);
    }
}
