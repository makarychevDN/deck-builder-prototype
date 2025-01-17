using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deal Damage Battle Effect", menuName = "ScriptableObjects/Deal Damage Battle Effect", order = 1)]
public class DealDamageBattleEffect : BaseBattleEffect
{
    [SerializeField] private int damage;

    public override void UseEffectOnTargets(List<Character> targets)
    {
        targets.ForEach(target => target.TakeDamage(damage));
    }
}
