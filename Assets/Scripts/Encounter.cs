using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    [SerializeField] private BaseInput playerInput;
    [SerializeField] private BaseInput enemyInput;

    private void Awake()
    {
        Init(playerInput);
    }

    public void Init(BaseInput playerInput)
    {
        this.playerInput = playerInput;
        playerInput.Init(enemyInput);
        enemyInput.Init(playerInput);

        playerInput.OnTurnEnded.AddListener(enemyInput.StartTurn);
        enemyInput.OnTurnEnded.AddListener(playerInput.StartTurn);

        playerInput.StartTurn();
    }
}
