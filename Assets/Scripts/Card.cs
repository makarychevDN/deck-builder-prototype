using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int cost;
    [SerializeField] private Animator animator;
    [SerializeField] private BaseBattleEffect battleEffect;
    private PlayerInput playerInput;
    private bool isSelected = false;

    public BaseBattleEffect BattleEffect => battleEffect;

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

    public async void OnPointerClick(PointerEventData eventData)
    {
        /*if(!isSelected)
        {
            playerInput.SelectCard(this);
        }
        else
        {
            playerInput.UnselectCard(this);
        }

        isSelected = !isSelected;*/

        await playerInput.DiscardCard(this);
        await battleEffect.UseEffectOnTargets(playerInput.EnemyTeam.CharactersList);
    }
}
