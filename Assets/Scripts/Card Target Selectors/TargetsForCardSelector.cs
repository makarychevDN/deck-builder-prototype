using System.Collections.Generic;
using UnityEngine;

public abstract class TargetsForCardSelector : MonoBehaviour
{
    protected PlayerInput playerInput;

    public void Init(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
    }

    public abstract List<Character> SelectTargets();
}
