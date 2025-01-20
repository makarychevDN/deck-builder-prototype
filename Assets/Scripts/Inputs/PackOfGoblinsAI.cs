using System.Collections.Generic;

public class PackOfGoblinsAI : BaseInput
{
    public async override void StartTurn()
    {
        base.StartTurn();

        foreach (var character in charactersList)
        {
            var selectedEffect = character.AvailableBattleEffects.GetRandomElement();
            var targets = selectedEffect is BlockBattleEffect ? new List<Character>() { character } : enemyTeam.CharactersList;
            await selectedEffect.UseEffectOnTargets(targets);
        }

        EndTurn();
    }
}
