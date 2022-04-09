using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Class defining the behaviour for a roof. When should or should not hide if the player is around
public class RoofInteraction : MonoBehaviour
{
    [SerializeField] TriggerHelper triggerHelper;
    [SerializeField] Renderer roof;
    [SerializeField] float duration;
    [SerializeField] Color hideColor;
    [SerializeField] Color showColor;
    private Color currentColor = Color.white;

    private void Awake()
    {
        triggerHelper.onTriggerEnter += HideRoof;
        triggerHelper.onTriggerExit += ShowRoof;
    }

    public void ShowRoof(Collider col)
    {   
        DOTween.To(() => currentColor, x => currentColor = x, showColor, duration).OnUpdate(SetColor);
    }

    public void HideRoof(Collider col)
    {
        DOTween.To(() => currentColor, x => currentColor = x, hideColor, duration).OnUpdate(SetColor);
    }

    private void SetColor()
    {
        roof.material.SetColor("_Color", currentColor);
    }

    private void OnDestroy()
    {
        triggerHelper.onTriggerEnter -= HideRoof;
        triggerHelper.onTriggerExit -= ShowRoof;
    }
}
