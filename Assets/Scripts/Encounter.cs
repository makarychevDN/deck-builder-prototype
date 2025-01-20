using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    [SerializeField] private BaseInput enemyInput;
    private PlayerInput playerInput;

    public BaseInput EnemyInput => enemyInput;

    public void Init(PlayerInput playerInput)
    {
        this.playerInput = playerInput;
        playerInput.Init(enemyInput);
        enemyInput.Init(playerInput);

        playerInput.OnTurnEnded.AddListener(enemyInput.StartTurn);
        enemyInput.OnTurnEnded.AddListener(playerInput.StartTurn);
        enemyInput.OnLose.AddListener(playerInput.DisableEndTurnButton);

        playerInput.StartTurn();
    }

    public void Uninit()
    {
        playerInput.OnTurnEnded.RemoveListener(enemyInput.StartTurn);
        enemyInput.OnTurnEnded.RemoveListener(playerInput.StartTurn);
        enemyInput.OnLose.RemoveListener(playerInput.DisableEndTurnButton);
    }
}
