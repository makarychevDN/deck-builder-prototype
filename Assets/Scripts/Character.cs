using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int currentBlock;
    [SerializeField] private Animator animator;
    [SerializeField] private List<BaseBattleEffect> availableBattleEffects;
    [SerializeField] private GameObject selectionCell;

    [Header("Intentions Setup")]
    [SerializeField] private GameObject intentionParent;
    [SerializeField] private Image intentionIcon;
    [SerializeField] private Sprite intentionToAttackSprite;
    [SerializeField] private Sprite intentionToDefenceSprite;
    [SerializeField] private Sprite intentionToHealSprite;
    [SerializeField] private TMP_Text intentionTextValue;


    public UnityEvent<int> OnCurrentHealthChanged = new();
    public UnityEvent<int> OnCurrentBlockChanged = new();
    public UnityEvent<Character> OnDeath = new();

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

        OnCurrentBlockChanged.Invoke(currentBlock);

        if (damage == 0)
            return;

        currentHealth -= damage;
        OnCurrentHealthChanged?.Invoke(currentHealth);
        animator.SetTrigger("Take Damage");

        if (currentHealth > 0)
            return;
        
        OnDeath.Invoke(this);
    }

    public void TakeHealing(int healingValue)
    {
        currentHealth += healingValue;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnCurrentHealthChanged?.Invoke(currentHealth);
        animator.SetTrigger("Take Healing");
    }

    public void TakeBlock(int blockValue)
    {
        currentBlock += blockValue;
        OnCurrentBlockChanged.Invoke(currentBlock);
    }

    private void ResetBlock()
    {
        currentBlock = 0;
        OnCurrentBlockChanged.Invoke(currentBlock);
    }

    public void EnableSelectionCell(bool value)
    {
        selectionCell.SetActive(value);
    }

    public void DisplayIntention(BaseBattleEffect effect)
    {
        intentionTextValue.text = "10";
        intentionParent.SetActive(true);
        intentionIcon.sprite = GetSpriteByBattleEffect(effect);
    }

    private Sprite GetSpriteByBattleEffect(BaseBattleEffect effect)
    {
        if (effect is DealDamageBattleEffect)
            return intentionToAttackSprite;

        if (effect is BlockBattleEffect)
            return intentionToDefenceSprite;

        if (effect is HealingBattleEffect)
            return intentionToHealSprite;

        return null;
    }
}
