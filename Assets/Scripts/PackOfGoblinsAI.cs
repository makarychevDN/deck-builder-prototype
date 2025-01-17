public class PackOfGoblinsAI : BaseInput
{
    public async override void StartTurn()
    {
        base.StartTurn();

        print("goblins attack!");
        foreach (var character in charactersList)
        {
            await character.AvailableBattleEffects[0].UseEffectOnTargets(enemyTeam.CharactersList);
        }

        EndTurn();
    }
}
