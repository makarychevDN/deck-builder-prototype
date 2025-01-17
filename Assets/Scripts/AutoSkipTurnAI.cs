using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSkipTurnAI : BaseInput
{
    public override void StartTurn()
    {
        base.StartTurn();
        print("enemies have ended it's turn");
        EndTurn();
    }
}
