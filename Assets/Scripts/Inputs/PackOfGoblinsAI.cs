using System.Collections.Generic;

public class PackOfGoblinsAI : BaseAIInput
{
    public async override void StartTurn()
    {
        base.StartTurn();

        foreach (var character in charactersList)
        {
            var targets = preparedBattleEffects[character] is BlockBattleEffect ? 
                new List<Character>() { character } : enemyTeam.CharactersList;
            await preparedBattleEffects[character].UseEffectOnTargets(targets);
        }

        EndTurn();
    }

    protected override void PrepareIntentions()
    {
        foreach (var character in charactersList)
        {
            if (preparedBattleEffects.ContainsKey(character))
                preparedBattleEffects[character] = character.AvailableBattleEffects.GetRandomElement();
            else
                preparedBattleEffects.Add(character, character.AvailableBattleEffects.GetRandomElement());

            character.DisplayIntention(preparedBattleEffects[character]);
        }
    }
}
