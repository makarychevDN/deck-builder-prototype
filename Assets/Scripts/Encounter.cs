using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    [SerializeField] private BaseInput playerInput;
    [SerializeField] private BaseInput enemyInput;

    public BaseInput EnemyInput => enemyInput;

    public void Init(BaseInput playerInput)
    {
        this.playerInput = playerInput;
        playerInput.Init(enemyInput);
        enemyInput.Init(playerInput);

        playerInput.OnTurnEnded.AddListener(enemyInput.StartTurn);
        enemyInput.OnTurnEnded.AddListener(playerInput.StartTurn);

        playerInput.StartTurn();
    }

    public void Uninit()
    {
        playerInput.OnTurnEnded.RemoveListener(enemyInput.StartTurn);
        enemyInput.OnTurnEnded.RemoveListener(playerInput.StartTurn);
    }
}
