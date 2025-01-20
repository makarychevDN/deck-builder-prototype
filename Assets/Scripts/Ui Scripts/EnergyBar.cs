using TMPro;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        playerInput.OnEnergyUpdated.AddListener(DisplayValue);
    }

    private void DisplayValue(int energyValue)
    {
        label.text = energyValue.ToString();
    }
}
