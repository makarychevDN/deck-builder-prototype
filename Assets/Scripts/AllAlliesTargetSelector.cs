using System.Collections.Generic;

public class AllAlliesTargetSelector : TargetsForCardSelector
{
    public override List<Character> SelectTargets()
    {
        return playerInput.CharactersList;
    }
}
