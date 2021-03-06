using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
//Simple class acting as an accesor for some ui components. 
public class UiController : MonoBehaviour
{
    [SerializeField] LivingEntity livingEntity;
    [SerializeField] Slider healthSlider;

    private void Awake()
    {
        livingEntity.onLifeChanged += UpdateHealth; 
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
