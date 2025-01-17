using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private List<BaseBattleEffect> availableBattleEffects;
    public UnityEvent<int> onCurrentHealthChanged = new();

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public List<BaseBattleEffect> AvailableBattleEffects => availableBattleEffects;

    private void Awake()
    {
        availableBattleEffects.ForEach(battleEffect => 
        battleEffect.OnEffectWithAnimationTypeUsed.AddListener(PlayAnimation)
        );
    }

    private void PlayAnimation(string animationType)
    {
        animator.SetTrigger(animationType);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        onCurrentHealthChanged?.Invoke(currentHealth);
        animator.SetTrigger("Take Damage");
    }
}
