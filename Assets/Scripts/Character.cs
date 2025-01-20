using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int currentBlock;
    [SerializeField] private Animator animator;
    [SerializeField] private List<BaseBattleEffect> availableBattleEffects;
    [SerializeField] private GameObject selectionCell;
    public UnityEvent<int> onCurrentHealthChanged = new();
    public UnityEvent<int> onCurrentBlockChanged = new();

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public List<BaseBattleEffect> AvailableBattleEffects => availableBattleEffects;

    public void Init(UnityEvent OnItsTurnStarted)
    {
        OnItsTurnStarted.AddListener(ResetBlock);

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
        var hashedDamage = damage;
        damage -= currentBlock;
        currentBlock -= hashedDamage;
        currentBlock = Mathf.Clamp(currentBlock, 0, 999);
        damage = Mathf.Clamp(damage, 0, 999);
        onCurrentBlockChanged.Invoke(currentBlock);

        if (damage == 0)
            return;

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

    public void TakeBlock(int blockValue)
    {
        currentBlock += blockValue;
        onCurrentBlockChanged.Invoke(currentBlock);
    }

    private void ResetBlock()
    {
        currentBlock = 0;
        onCurrentBlockChanged.Invoke(currentBlock);
    }

    public void EnableSelectionCell(bool value)
    {
        selectionCell.SetActive(value);
    }
}
