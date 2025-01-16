using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Character character;
    [SerializeField] private TMP_Text healthBar;

    private void Awake()
    {
        slider.maxValue = character.MaxHealth;
        slider.value = character.CurrentHealth;
        character.onCurrentHealthChanged.AddListener(UpdateSliderValue);
    }

    private void UpdateSliderValue(int updatedValue)
    {
        slider.value = updatedValue;
        healthBar.text = $"{character.CurrentHealth} / {character.MaxHealth}";
    }
}
