using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] bool hasRoof;

    [SerializeField] Transform[] roof;

    [SerializeField] Transform door;

    public void OpenDoor()
    {
        door.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
    }

    public void CloseDoor()
    {
        door.rotation = Quaternion.identity;
    }

    public void OpenRoof()
    {
        for (int i = 0; i < roof.Length; i++)
        {
            roof[i].gameObject.SetActive(true);
        }
    }

    public void CloseRoof()
    {
        for (int i = 0; i < roof.Length; i++)
        {
            roof[i].gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            OpenDoor();
            CloseRoof();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
            OpenRoof();

        }
    }
}
