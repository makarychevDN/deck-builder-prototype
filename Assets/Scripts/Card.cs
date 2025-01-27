using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int energyCost;
    [SerializeField] private Animator animator;
    [SerializeField] private BaseBattleEffect battleEffect;
    [SerializeField] private SelectedCardBehaviourTypes selectedCardBehaviourType;
    [SerializeField] private TargetsForCardSelector targetsForCardSelector;
    [SerializeField] private TMP_Text costLabel;
    private PlayerInput playerInput;

    public BaseBattleEffect BattleEffect => battleEffect;
    public int EnergyCost => energyCost;
    public SelectedCardBehaviourTypes SelectedCardBehaviourType => selectedCardBehaviourType;
    public TargetsForCardSelector TargetsForCardSelector => targetsForCardSelector;

    public void Init(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
        targetsForCardSelector.Init(playerInput);
        costLabel.text = energyCost.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (playerInput.SelectedCard == this)
            return;

        Hover(false);
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

    public void Hover(bool isHovered)
    {
        animator.SetBool("Hovered", isHovered);
    }
}

public enum SelectedCardBehaviourTypes
{
    followMouse = 1,
    goToTheCenterOfHand = 10
}
