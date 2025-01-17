using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private List<BaseBattleEffect> availableBattleEffects;
    public UnityEvent<int> onCurrentHealthChanged = new();

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public List<BaseBattleEffect> AvailableBattleEffects => availableBattleEffects;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        onCurrentHealthChanged?.Invoke(currentHealth);
    }
}
