public class PackOfGoblinsAI : BaseInput
{
    public override void StartTurn()
    {
        base.StartTurn();

        print("goblins attack!");
        foreach (var character in charactersList)
        {
            character.AvailableBattleEffects[0].UseEffectOnTargets(enemyTeam.CharactersList);
        }

        EndTurn();
    }
}
