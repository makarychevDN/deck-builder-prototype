using UnityEngine;

public class PlayerInput : BaseInput
{
    private void Update()
    {
        if (!isMyTurn)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
            print("player has ended it's turn");
        }
    }
}
