using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public UnityEvent<int> onCurrentHealthChanged = new();

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        onCurrentHealthChanged?.Invoke(currentHealth);
    }
}
