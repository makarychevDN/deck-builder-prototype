using System.Collections.Generic;

public class AllEnemiesTargetSelector : TargetsForCardSelector
{
    public override List<Character> SelectTargets()
    {
        return playerInput.EnemyTeam.CharactersList;
    }
}
