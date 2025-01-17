using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int cost;
    [SerializeField] private Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Hovered", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Hovered", false);
    }
}
