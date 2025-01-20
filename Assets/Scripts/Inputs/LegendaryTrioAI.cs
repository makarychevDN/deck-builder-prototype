using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LegendaryTrioAI : BaseInput
{
    [SerializeField] private Character shieldHolder;
    [SerializeField] private Character archer;
    [SerializeField] private Character cleric;

    public async override void StartTurn()
    {
        base.StartTurn();

        if (shieldHolder != null)
        {
            await shieldHolder.AvailableBattleEffects[0].UseEffectOnTarget(charactersList.GetRandomElement());
        }

        if (archer != null)
        {
            await archer.AvailableBattleEffects[0].UseEffectOnTargets(enemyTeam.CharactersList);
        }

        if (cleric != null)
        {
            await cleric.AvailableBattleEffects[0].UseEffectOnTarget(charactersList.OrderBy(character => character.CurrentHealth).First());
        }

        EndTurn();
    }
}
