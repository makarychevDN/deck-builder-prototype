using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DealDamageBattleEffect : BaseBattleEffect
{
    [SerializeField] private int damage;
    [SerializeField] private int timeForWholeProcess = 1500;
    [SerializeField] private int timeBeforeImpact = 200;

    public override async Task UseEffectOnTargets(List<Character> targets)
    {
        OnEffectWithAnimationTypeUsed.Invoke(animationType);
        await Task.Delay(timeBeforeImpact);
        targets.ForEach(target => target.TakeDamage(damage));
        await Task.Delay(timeForWholeProcess - timeBeforeImpact);
    }
}
