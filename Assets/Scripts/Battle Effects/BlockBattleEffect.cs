using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BlockBattleEffect : BaseBattleEffect
{
    [SerializeField] private int blockValue;

    public override int GetValue() => blockValue;

    public override Task UseEffectOnTarget(Character target) => UseEffectOnTargets(new List<Character> { target });

    public override async Task UseEffectOnTargets(List<Character> targets)
    {
        OnEffectWithAnimationTypeUsed.Invoke(animationType);
        await Task.Delay(timeBeforeImpact);
        targets.ForEach(target => target.TakeBlock(blockValue));
        await Task.Delay(timeForWholeProcess - timeBeforeImpact);
    }
}