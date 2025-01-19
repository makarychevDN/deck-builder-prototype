using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : BaseInput
{
    [SerializeField] private List<Card> cards;
    [SerializeField] private List<Card> drawPile;
    [SerializeField] private List<Card> hand;
    [SerializeField] private List<Card> discardPile;

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

    public override void Init(BaseInput enemyTeam)
    {
        base.Init(enemyTeam);

        drawPile.AddRange(hand);
        drawPile.AddRange(discardPile);
        drawPile.Shuffle();

        hand.Clear();
        discardPile.Clear();
    }
}
