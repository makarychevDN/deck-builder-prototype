using System.Collections.Generic;

public class PunchingDummyAI : BaseInput
{
    public async override void StartTurn()
    {
        base.StartTurn();

        foreach (var character in charactersList)
        {
            await character.AvailableBattleEffects.GetRandomElement().UseEffectOnTargets(new List<Character>() { character });
        }

        EndTurn();
    }
}