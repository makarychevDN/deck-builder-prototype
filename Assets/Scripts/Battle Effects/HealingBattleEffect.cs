using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HealingBattleEffect : BaseBattleEffect
{
    [SerializeField] private int healingValue;

    public override Task UseEffectOnTarget(Character target) => UseEffectOnTargets(new List<Character> { target });

    public override async Task UseEffectOnTargets(List<Character> targets)
    {
        OnEffectWithAnimationTypeUsed.Invoke(animationType);
        await Task.Delay(timeBeforeImpact);
        targets.ForEach(target => target.TakeHealing(healingValue));
        await Task.Delay(timeForWholeProcess - timeBeforeImpact);
    }
}
