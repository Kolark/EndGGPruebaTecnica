using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DoorInteraction : MonoBehaviour, IUnlockable
{
    [SerializeField] TriggerHelper openTrigger;
    [SerializeField] Collider unlockDoorCollider;
    [SerializeField] Transform door;
    [SerializeField, Range(0, 360)] float closedRotation;
    [SerializeField, Range(0, 360)] float openRotation;
    [SerializeField] float duration;

    [SerializeField] bool isUnlocked = false;

    public bool IsUnlocked => true;

    private void Awake()
    {
        openTrigger.SetCollider(isUnlocked);
        unlockDoorCollider.enabled = !isUnlocked;
        openTrigger.onTriggerEnter += OpenDoor;
        openTrigger.onTriggerExit += CloseDoor;
    }

    public void OpenDoor(Collider collider)
    {
        door.DOLocalRotate(Vector3.up * openRotation, duration).SetEase(Ease.OutBounce);
    }

    public void CloseDoor(Collider collider)
    {
        door.DOLocalRotate(Vector3.up * closedRotation, duration).SetEase(Ease.OutBounce);
    }
    private void OnDestroy()
    {
        openTrigger.onTriggerEnter -= OpenDoor;
        openTrigger.onTriggerExit -= CloseDoor;
    }

    public bool Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            openTrigger.SetCollider(isUnlocked);
            unlockDoorCollider.enabled = !isUnlocked;
            return true;
        }
        else return false;

    }

    private void OnValidate()
    {
        openTrigger.SetCollider(isUnlocked);
        unlockDoorCollider.enabled = !isUnlocked;
    }
}
