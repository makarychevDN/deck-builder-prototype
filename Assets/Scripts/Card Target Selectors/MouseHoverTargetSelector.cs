using System.Collections.Generic;
using UnityEngine;

public class MouseHoverTargetSelector : TargetsForCardSelector
{
    [SerializeField] private bool enemiesIncluded = true;
    [SerializeField] private bool alliesIncluded;

    public override List<Character> SelectTargets()
    {
        var targets = TryToGetCharacterByRaycast();
        return targets == null ? null : new List<Character> { targets };
    }

    private Character TryToGetCharacterByRaycast()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            var character = hit.collider.gameObject.GetComponent<Character>();
            return FilterSelectedTarget(character);
        }
        else
        {
            return null;
        }
    }

    private Character FilterSelectedTarget(Character target)
    {
        if (!alliesIncluded && playerInput.CharactersList.Contains(target))
            return null;

        if (!enemiesIncluded && playerInput.OppositeTeam.CharactersList.Contains(target))
            return null;

        return target;
    }
}