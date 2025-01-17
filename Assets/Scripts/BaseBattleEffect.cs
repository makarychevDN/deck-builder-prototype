using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBattleEffect : ScriptableObject
{
    public abstract void UseEffectOnTargets(List<Character> targets);
}
