using System.Collections.Generic;

public class PunchingDummyAI : BaseAIInput
{
    public async override void StartTurn()
    {
        base.StartTurn();

        foreach (var character in charactersList)
        {
            await preparedBattleEffects[character].UseEffectOnTargets(new List<Character>() { character });
        }

        EndTurn();
    }

    protected override void PrepareIntentions()
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