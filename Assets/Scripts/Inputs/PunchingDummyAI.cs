using System;
using System.Collections.Generic;
using UnityEngine;

public class PunchingDummyAI : BaseInput
{
    private Dictionary<Character, BaseBattleEffect> preparedBattleEffects = new();

    public override void Init(BaseInput enemyTeam)
    {
        base.Init(enemyTeam);
        PrepareIntentions();
    }

    public async override void StartTurn()
    {
        base.StartTurn();

        foreach (var character in charactersList)
        {
            await preparedBattleEffects[character].UseEffectOnTargets(new List<Character>() { character });
        }

        EndTurn();
    }

    private void PrepareIntentions()
    {
        foreach (var character in charactersList)
        {
            if (preparedBattleEffects.ContainsKey(character))
            {
                preparedBattleEffects[character] = character.AvailableBattleEffects.GetRandomElement();
            }
            else
            {
                preparedBattleEffects.Add(character, character.AvailableBattleEffects.GetRandomElement());
            }

            character.DisplayIntention(preparedBattleEffects[character]);
        }
    }
}