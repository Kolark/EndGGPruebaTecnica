using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UiController : MonoBehaviour
{
    [SerializeField] LivingEntity livingEntity;
    [SerializeField] Slider healthSlider;

    private void Awake()
    {
        livingEntity.onDamage += UpdateHealth; 
    }

    private void Start()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        float newValue = (livingEntity.CurrentHealth / livingEntity.MaxHealth);
        healthSlider.DOValue(newValue, 0.15f);
    }

}
