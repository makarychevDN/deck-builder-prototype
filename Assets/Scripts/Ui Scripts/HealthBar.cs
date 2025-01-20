using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Character character;
    [SerializeField] private TMP_Text textHealthValue;

    private void Awake()
    {
        slider.maxValue = character.MaxHealth;
        slider.value = character.CurrentHealth;
        character.OnCurrentHealthChanged.AddListener(UpdateSliderValue);
        UpdateSliderValue(character.CurrentHealth);
    }

    private void UpdateSliderValue(int updatedValue)
    {
        slider.value = updatedValue;
        textHealthValue.text = $"{character.CurrentHealth} / {character.MaxHealth}";
    }
}
