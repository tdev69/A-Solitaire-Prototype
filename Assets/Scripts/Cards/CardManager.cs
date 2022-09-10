using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Sprite[] cardSprites = new Sprite[56];

    [Header("Information between objects")]
    [SerializeField] private SOAllCards allCards = null;
    [SerializeField] private SOLevelCards levelCards = null;

    [Header("Game Events")]
    [SerializeField] private SOGameEvent countCards = null;
    [SerializeField] private SOGameEvent passCardInLevel = null;

    [Header("Leave empty for random card:")]
    [SerializeField] private SOCardInfo cardInfo = null;
    [SerializeField] private bool showBackAtStart = true;
    
    private bool isInClosedDeck = false;
    private bool isFlipped = false;
    private BoxCollider2D bc2d = null;
    private CardStateManager csm = null;
    private DisplayManager displayManager = null;
    private FlipInPlace fip = null;
    private Tween flipTweener = null;
    

    public void GetCardInfoListener()
    {
        GetCardInfo();
        SetSpriteAtStart();
    }

    public CardSymbol GetCardSymbol()
    {
        return cardInfo.GetCardSymbol();
    }

    public CardValue GetCardValue()
    {
        return cardInfo.GetCardValue();
    }

    public bool GetIsInClosedDeck()
    {
        return isInClosedDeck;
    }

    public bool GetShowBackAtStart()
    {
        return showBackAtStart;
    }

    public void MoveToOpenDeck()
    {
        bc2d.enabled = false;
        CardSymbol thisCardSymbol = GetCardSymbol();
        CardValue thisCardValue = GetCardValue();
        displayManager.SetOrderInLayer(allCards.GetNumberOfCardsInOpenDeck());

        if(GetIsInClosedDeck() == true)
        {
            SetIsInClosedDeck(false);
            displayManager.ShowCardFace((thisCardSymbol, thisCardValue));
            allCards.MoveFromClosedDeckToOpenDeck();
        }

        else
        {
            allCards.PutCardInOpenDeck((thisCardSymbol, thisCardValue));
            levelCards.RemoveCardFromInLevel(cardInfo);
        }

        fip.SetIsFlipped(true);
        countCards.Raise();
    }

    public void RemoveCardFromAvailableCards()
    {
        if(cardInfo != null)
        {
            (CardSymbol, CardValue) aCard;
            aCard.Item1 = GetCardSymbol();
            aCard.Item2 = GetCardValue();
            int index = allCards.GetIndexFromAvailableCards(aCard);
            allCards.GetCardFromAvailableCards(index); //Removes already set card from list of available cards
            SetSpriteAtStart();
        }
    }

    /// <summary>
    /// Set the Tween to follow to change the card sprite at the right time
    /// </summary>
    /// <param name="aTween">Tween</param>
    public void SetFlipTweener(Tween aTween)
    {
        flipTweener = aTween;
    }

    public void SetInLevelCards()
    {
        if(GetIsInClosedDeck() == false)
        {
            levelCards.SetCardInLevel(cardInfo);
        }
    }

    public void SetIsInClosedDeck(bool aBool)
    {
        isInClosedDeck = aBool;
    }

    /// <summary>
    /// Sets the spriteRenderer Order in layer to the int passed in argument.
    /// Higher int are displayed first
    /// </summary>
    /// <param name="aPriority"></param>
    public void SetLayerPriority(int aPriority)
    {
        displayManager.SetOrderInLayer(aPriority);
    }

    public void TriggerClickBehaviour()
    {
        csm.TriggerClickBehaviour(transform.gameObject);
    }

    private void GetCardInfo()
    {
        if (cardInfo == null)
        {
            cardInfo = allCards.GetRandomCard();
        }
    }

    private void SetSpriteAtStart() 
    {
        if(showBackAtStart)
        {
            displayManager.ShowCardBack();
        }

        else 
        {
            CardSymbol thisCardSymbol = cardInfo.GetCardSymbol();
            CardValue thisCardValue = cardInfo.GetCardValue();
            displayManager.ShowCardFace((thisCardSymbol, thisCardValue));
        }
    }

    private void SetInClosedDeck()
    {
        if(transform.parent.name == "ClosedDeck")
        {
            SetIsInClosedDeck(true);
            GetCardInfo();
            CardSymbol thisCardSymbol = cardInfo.GetCardSymbol();
            CardValue thisCardValue = cardInfo.GetCardValue();
            allCards.AddCardToClosedDeck((thisCardSymbol, thisCardValue));
            displayManager.SetOrderInLayer(allCards.GetNumberOfCardsInClosedDeck()); //ensures new cards are on top and order of cards is respected
            SetSpriteAtStart();
        }
    }

    private void Awake()
    {
        displayManager = GetComponent<DisplayManager>();
        SetInClosedDeck();
        bc2d = GetComponent<BoxCollider2D>();
        csm = GetComponent<CardStateManager>();
        fip = GetComponent<FlipInPlace>();
    }

    private void Update()
    {
        if (flipTweener != null 
            && isFlipped == false
            && flipTweener.ElapsedPercentage() >= 0.5f)
        {
            isFlipped = true;
            MoveToOpenDeck();
        }
    }
}
