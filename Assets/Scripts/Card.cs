using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int cost;
    [SerializeField] private Animator animator;
    [SerializeField] private BaseBattleEffect battleEffect;
    [SerializeField] private SelectedCardBehaviourTypes selectedCardBehaviourType;
    private PlayerInput playerInput;
    private bool isSelected = false;

    public BaseBattleEffect BattleEffect => battleEffect;
    public int Cost => cost;
    public SelectedCardBehaviourTypes SelectedCardBehaviourType => selectedCardBehaviourType;

    public void Init(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
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
}

public enum SelectedCardBehaviourTypes
{
    followMouse = 1,
    goToTheCenterOfHand = 10
}
