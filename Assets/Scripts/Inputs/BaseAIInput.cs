using System.Collections.Generic;

public abstract class BaseAIInput : BaseInput
{
    protected Dictionary<Character, BaseBattleEffect> preparedBattleEffects = new();

    public override void Init(BaseInput enemyTeam)
    {
        base.Init(enemyTeam);
        PrepareIntentions();
    }

    protected abstract void PrepareIntentions();

    public override void RemoveCharacter(Character character)
    {
        base.RemoveCharacter(character);

        if(preparedBattleEffects.ContainsKey(character))
            preparedBattleEffects.Remove(character);
    }

    public override void EndTurn()
    {
        base.EndTurn();
        PrepareIntentions();
    }
}
