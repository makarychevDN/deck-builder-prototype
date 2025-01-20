using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DealDamageBattleEffect : BaseBattleEffect
{
    [SerializeField] private int damage;

    public override int GetValue() => damage;

    public override Task UseEffectOnTarget(Character target) => UseEffectOnTargets(new List<Character> { target });

    public override async Task UseEffectOnTargets(List<Character> targets)
    {
        OnEffectWithAnimationTypeUsed.Invoke(animationType);
        await Task.Delay(timeBeforeImpact);
        targets.ForEach(target => target.TakeDamage(damage));
        await Task.Delay(timeForWholeProcess - timeBeforeImpact);
    }
}
