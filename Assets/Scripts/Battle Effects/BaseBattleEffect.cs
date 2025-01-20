using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseBattleEffect : MonoBehaviour
{
    [SerializeField] protected string animationType;
    [SerializeField] protected int timeForWholeProcess = 1500;
    [SerializeField] protected int timeBeforeImpact = 200;

    public UnityEvent<string> OnEffectWithAnimationTypeUsed = new();

    public abstract Task UseEffectOnTargets(List<Character> targets);

    public abstract Task UseEffectOnTarget(Character target);
}
