using TMPro;
using UnityEngine;

public class BlockIcon : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private GameObject parentToEnable;
    [SerializeField] private TMP_Text textBlockValue;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        character.OnCurrentBlockChanged.AddListener(UpdateBlockValue);
    }

    private void UpdateBlockValue(int updatedValue)
    {
        var needToShowBlockIcon = updatedValue > 0;
        parentToEnable.gameObject.SetActive(needToShowBlockIcon);

        if (!needToShowBlockIcon)
            return;

        textBlockValue.text = updatedValue.ToString();
        animator.SetTrigger("Block Changed");
    }
}
