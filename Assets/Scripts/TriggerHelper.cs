using System;
using UnityEngine;

//Helping class notifying when the current object collider has been triggeredEnter or triggeredExit
[RequireComponent(typeof(Collider))]
public class TriggerHelper : MonoBehaviour
{

    public Action<Collider> onTriggerExit;
    public Action<Collider> onTriggerEnter;
    private Collider col;
    public Collider Col
    {
        get
        {
            if(col == null)
            {
                col = GetComponent<Collider>();
            }

            return col;
        }
    }

    public void SetCollider(bool state)
    {
        Col.enabled = state;
    }

    private void OnTriggerEnter(Collider col)
    {
        onTriggerEnter?.Invoke(col);
    }

    private void OnTriggerExit(Collider col)
    {
        onTriggerExit?.Invoke(col);
    }
}
