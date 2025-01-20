using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private List<BaseBattleEffect> availableBattleEffects;
    [SerializeField] private GameObject selectionCell;
    public UnityEvent<int> onCurrentHealthChanged = new();

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public List<BaseBattleEffect> AvailableBattleEffects => availableBattleEffects;

    public void Init()
    {
        availableBattleEffects.ForEach(battleEffect => 
            battleEffect.OnEffectWithAnimationTypeUsed.AddListener(PlayAnimation));
    }

    public void AddAvailableBattleEffect(BaseBattleEffect battleEffect)
    {
        availableBattleEffects.Add(battleEffect);
        battleEffect.OnEffectWithAnimationTypeUsed.AddListener(PlayAnimation);
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

    public void TakeHealing(int healingValue)
    {
        currentHealth += healingValue;
        onCurrentHealthChanged?.Invoke(currentHealth);
        animator.SetTrigger("Take Healing");
    }

    public void EnableSelectionCell(bool value)
    {
        selectionCell.SetActive(value);
    }
}
