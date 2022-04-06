using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DoorInteraction : MonoBehaviour
{
    [SerializeField] TriggerHelper triggerHelper;
    [SerializeField] Transform door;
    [SerializeField, Range(0, 360)] float closedRotation;
    [SerializeField, Range(0, 360)] float openRotation;
    [SerializeField] float duration;

    private void Awake()
    {
        triggerHelper.onTriggerEnter += OpenDoor;
        triggerHelper.onTriggerExit += CloseDoor;
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
        triggerHelper.onTriggerEnter -= OpenDoor;
        triggerHelper.onTriggerExit -= CloseDoor;
    }
}
