using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LegendaryTrioAI : BaseAIInput
{
    [SerializeField] private Character shieldHolder;
    [SerializeField] private Character archer;
    [SerializeField] private Character cleric;

    public async override void StartTurn()
    {
        base.StartTurn();

        if (shieldHolder != null)
            await preparedBattleEffects[shieldHolder].UseEffectOnTarget(charactersList.GetRandomElement());

        if (archer != null)
            await preparedBattleEffects[archer].UseEffectOnTargets(enemyTeam.CharactersList);

        if (cleric != null)
            await preparedBattleEffects[cleric].UseEffectOnTarget(charactersList.OrderBy(character => character.CurrentHealth).First());

        EndTurn();
    }

    protected override void PrepareIntentions()
    {
        foreach (var character in charactersList)
        {
            if (preparedBattleEffects.ContainsKey(character))
                preparedBattleEffects[character] = character.AvailableBattleEffects[0];
            else
                preparedBattleEffects.Add(character, character.AvailableBattleEffects[0]);

            character.DisplayIntention(preparedBattleEffects[character]);
        }
    }
}
