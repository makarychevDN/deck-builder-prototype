using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int cost;
    [SerializeField] private Animator animator;
    [SerializeField] private BaseBattleEffect battleEffect;
    [SerializeField] private SelectedCardBehaviourTypes selectedCardBehaviourType;
    [SerializeField] private TargetsForCardSelector targetsForCardSelector;
    private PlayerInput playerInput;

    public BaseBattleEffect BattleEffect => battleEffect;
    public int Cost => cost;
    public SelectedCardBehaviourTypes SelectedCardBehaviourType => selectedCardBehaviourType;

    public void Init(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
        targetsForCardSelector.Init(playerInput);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Hovered", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Hovered", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        playerInput.HandleClickOnCard(this);
    }

    public void TryToUseCard()
    {
        var targets = targetsForCardSelector.SelectTargets();

        if (targets == null)
            return;

        battleEffect.UseEffectOnTargets(targets);
    }
}

public enum SelectedCardBehaviourTypes
{
    followMouse = 1,
    goToTheCenterOfHand = 10
}
