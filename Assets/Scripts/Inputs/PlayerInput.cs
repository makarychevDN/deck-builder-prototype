using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInput : BaseInput
{
    [Header("Stats")]
    [SerializeField] private int amountOfDrawingCardsPerTurn = 5;
    [SerializeField] private int currentEnergy = 3;
    [SerializeField] private int maxEnergyAmount = 3;

    [Header("Cards Interaction Setup")]
    [SerializeField] private int timeForCardsMovementBetweenPiles = 200;
    [SerializeField] private int heightOfUnselectCardZone = 250;

    [Header("Children Links")]
    [SerializeField] private GameObject targetSelectorArrow;
    [SerializeField] private GameObject shadingCardsPanel;
    [SerializeField] private Transform immovableSelectedCardPoint;
    [SerializeField] private Transform drawPileCardsParent;
    [SerializeField] private Transform handCardsParent;
    [SerializeField] private Transform discardPileCardsParent;
    [SerializeField] private Button endTurnButton;

    [Header("Debug Piles")]
    [SerializeField] private List<Card> drawPile;
    [SerializeField] private List<Card> hand;
    [SerializeField] private List<Card> discardPile;

    private Card selectedCard;

    public UnityEvent<int> OnEnergyUpdated = new();

    public override void Init(BaseInput enemyTeam)
    {
        base.Init(enemyTeam);

        endTurnButton.onClick.AddListener(EndTurn);

        SetAllCardsToDrawPile();
        foreach(var card in drawPile) 
        { 
            card.Init(this);
            charactersList[0].AddAvailableBattleEffect(card.BattleEffect);
        }
        drawPile.Shuffle();
    }

    public override async void StartTurn()
    {
        base.StartTurn();
        await DrawCards(amountOfDrawingCardsPerTurn);
        currentEnergy = maxEnergyAmount;
        OnEnergyUpdated.Invoke(currentEnergy);
        endTurnButton.interactable = true;
    }

    public override async void EndTurn()
    {
        endTurnButton.interactable = false;
        base.EndTurn();
        await DiscardCards(hand.Count);
    }

    private void Update()
    {
        charactersList.ForEach(character => character.EnableSelectionCell(false));
        EnemyTeam.CharactersList.ForEach(character => character.EnableSelectionCell(false));
        targetSelectorArrow.SetActive(false);
        shadingCardsPanel.SetActive(false);

        if (!isMyTurn)
            return;

        if (selectedCard == null)
            return;

        ControlSelectedCard();
    }

    public void SetAllCardsToDrawPile()
    {
        drawPile.AddRange(hand);
        drawPile.AddRange(discardPile);

        foreach (Card card in drawPile)
        {
            card.transform.SetParent(drawPileCardsParent);
        }

        hand.Clear();
        discardPile.Clear();
    }

    public async Task DrawCards(int cardsAmount)
    {
        for(int i = 0; i < cardsAmount; i++)
        {
            await DrawCard();
        }
    }

    public async Task DrawCard()
    {
        if (drawPile.Count == 0)
        {
            await ReturnCardsFromDiscardPileToDrawPile();
            drawPile.Shuffle();
        }

        if (drawPile.Count == 0)
            return;

        await MoveCardToHand(drawPile[0], drawPile);
    }

    public async Task DiscardCards(int cardsAmount)
    {
        for (int i = 0; i < cardsAmount; i++)
        {
            await DiscardFirstCardInHand();
        }
    }

    public async Task DiscardFirstCardInHand()
    {
        if (hand.Count == 0)
            return;

        await MoveCardToDiscardPile(hand[0], hand);
    }

    public async Task DiscardCard(Card card)
    {
        if (!hand.Contains(card))
            return;

        endTurnButton.interactable = false;
        await MoveCardToDiscardPile(card, hand);
        endTurnButton.interactable = true;
    }

    public async Task ReturnCardsFromDiscardPileToDrawPile()
    {
        while (discardPile.Count > 0)
        {
            await MoveCardToDrawPile(discardPile[0], discardPile);
        }
    }

    public async Task MoveCardToDrawPile(Card card, List<Card> currentPile)
    {
        await MoveCardToNewPile(card, currentPile, drawPile, drawPileCardsParent, timeForCardsMovementBetweenPiles);
    }

    public async Task MoveCardToHand(Card card, List<Card> currentPile)
    {
        await MoveCardToNewPile(card, currentPile, hand, handCardsParent, timeForCardsMovementBetweenPiles);
    }

    public async Task MoveCardToDiscardPile(Card card, List<Card> currentPile)
    {
        await MoveCardToNewPile(card, currentPile, discardPile, discardPileCardsParent, timeForCardsMovementBetweenPiles);
    }

    public async Task MoveCardToNewPile(Card card, List<Card> currentPile, List<Card> targetPile, Transform newParent, int timeOfTransition)
    {
        currentPile.Remove(card);
        targetPile.Add(card);
        card.transform.SetParent(transform);
        card.transform.DOScale(newParent.localScale, (timeOfTransition * 0.001f));
        card.transform.DOMove(newParent.position, (timeOfTransition * 0.001f));
        await Task.Delay(timeOfTransition);
        card.transform.SetParent(newParent);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)newParent);
    }

    public void HandleClickOnCard(Card card)
    {
        if(selectedCard == null)
        {
            SelectCard(card);
            return;
        }
    }

    public void SelectCard(Card card)
    {
        if (card.EnergyCost > currentEnergy)
            return;

        card.transform.SetParent(transform);
        selectedCard = card;

        if (card.SelectedCardBehaviourType == SelectedCardBehaviourTypes.goToTheCenterOfHand)
            card.transform.position = immovableSelectedCardPoint.transform.position;
    }

    public void UnselectCard(Card card)
    {
        selectedCard = null;
        card.transform.SetParent(handCardsParent);
    }

    public void UseCard(Card card)
    {
        selectedCard = null;
        card.TryToUseCard();
        DiscardCard(card);
        currentEnergy -= card.EnergyCost;
        OnEnergyUpdated.Invoke(currentEnergy);
    }

    private void ControlSelectedCard()
    {
        shadingCardsPanel.SetActive(true);

        if (selectedCard.SelectedCardBehaviourType == SelectedCardBehaviourTypes.followMouse)
        {
            selectedCard.transform.position = Input.mousePosition;
        }
        else
        {
            targetSelectorArrow.gameObject.SetActive(true);
            PointArrowFromSelectedCardToMouse();
        }

        if (Input.mousePosition.y < heightOfUnselectCardZone)
        {
            UseCardUndernselectCardZone();
        }
        else
        {
            UseCardAboveUnselectCardZone();
        }
    }

    private void UseCardAboveUnselectCardZone()
    {
        var targets = selectedCard.TargetsForCardSelector.SelectTargets();

        if (targets == null || targets.Count == 0)
            return;

        targets.ForEach(character => character.EnableSelectionCell(true));

        if (Input.GetKeyDown(KeyCode.Mouse0))
            UseCard(selectedCard);
    }

    private void UseCardUndernselectCardZone()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            UnselectCard(selectedCard);
    }

    private void PointArrowFromSelectedCardToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 objectPosition = targetSelectorArrow.transform.position;

        Vector3 direction = mousePosition - objectPosition;
        float magnitude = Vector3.Magnitude(mousePosition - objectPosition);
        float angle = Vector2.SignedAngle(Vector2.right, direction);

        targetSelectorArrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        var arrowsRectTransform = (targetSelectorArrow.transform as RectTransform);
        arrowsRectTransform.sizeDelta = new Vector2(magnitude, arrowsRectTransform.sizeDelta.y);
    }
}
