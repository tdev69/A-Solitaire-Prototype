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
    private BoxCollider2D bc2d = null;
    private CardStateManager csm = null;
    private DisplayManager displayManager = null;
    private FlipInPlace fip = null;
    

    public void GetCardInfoListener()
    {
        this.GetCardInfo();
        this.SetSpriteAtStart();
    }

    public CardSymbol GetCardSymbol()
    {
        return this.cardInfo.GetCardSymbol();
    }

    public CardValue GetCardValue()
    {
        return this.cardInfo.GetCardValue();
    }

    public bool GetIsInClosedDeck()
    {
        return this.isInClosedDeck;
    }

    public bool GetShowBackAtStart()
    {
        return this.showBackAtStart;
    }

    public void MoveToOpenDeck()
    {
        this.bc2d.enabled = false;
        CardSymbol thisCardSymbol = this.GetCardSymbol();
        CardValue thisCardValue = this.GetCardValue();
        this.displayManager.SetOrderInLayer(this.allCards.GetNumberOfCardsInOpenDeck());

        if(this.GetIsInClosedDeck() == true)
        {
            this.SetIsInClosedDeck(false);
            this.displayManager.ShowCardFace((thisCardSymbol, thisCardValue));
            this.allCards.MoveFromClosedDeckToOpenDeck();
        }

        else
        {
            this.allCards.PutCardInOpenDeck((thisCardSymbol, thisCardValue));
            this.levelCards.RemoveCardFromInLevel(this.cardInfo);
        }

        this.fip.SetIsFlipped(true);
        this.countCards.Raise();
    }

    public void RemoveCardFromAvailableCards()
    {
        if(this.cardInfo != null)
        {
            (CardSymbol, CardValue) aCard;
            aCard.Item1 = this.GetCardSymbol();
            aCard.Item2 = this.GetCardValue();
            int index = this.allCards.GetIndexFromAvailableCards(aCard);
            this.allCards.GetCardFromAvailableCards(index); //Removes already set card from list of available cards
            this.SetSpriteAtStart();
        }
    }

    public void SetInLevelCards()
    {
        if(this.GetIsInClosedDeck() == false)
        {
            this.levelCards.SetCardInLevel(this.cardInfo);
        }
    }

    public void SetIsInClosedDeck(bool aBool)
    {
        this.isInClosedDeck = aBool;
    }

    /// <summary>
    /// Sets the spriteRenderer Order in layer to the int passed in argument.
    /// Higher int are displayed first
    /// </summary>
    /// <param name="aPriority"></param>
    public void SetLayerPriority(int aPriority)
    {
        this.displayManager.SetOrderInLayer(aPriority);
    }

    public void TriggerClickBehaviour()
    {
        this.csm.TriggerClickBehaviour(this.transform.gameObject);
    }

    private void GetCardInfo()
    {
        if (this.cardInfo == null)
        {
            this.cardInfo = this.allCards.GetRandomCard();
        }
    }

    private void SetSpriteAtStart() 
    {
        if(this.showBackAtStart)
        {
            this.displayManager.ShowCardBack();
        }

        else 
        {
            CardSymbol thisCardSymbol = this.cardInfo.GetCardSymbol();
            CardValue thisCardValue = this.cardInfo.GetCardValue();
            this.displayManager.ShowCardFace((thisCardSymbol, thisCardValue));
        }
    }

    private void SetInClosedDeck()
    {
        if(this.transform.parent.name == "ClosedDeck")
        {
            this.SetIsInClosedDeck(true);
            this.GetCardInfo();
            CardSymbol thisCardSymbol = this.cardInfo.GetCardSymbol();
            CardValue thisCardValue = this.cardInfo.GetCardValue();
            this.allCards.AddCardToClosedDeck((thisCardSymbol, thisCardValue));
            this.displayManager.SetOrderInLayer(this.allCards.GetNumberOfCardsInClosedDeck()); //ensures new cards are on top and order of cards is respected
            this.SetSpriteAtStart();
        }
    }

    private void Awake()
    {
        this.displayManager = GetComponent<DisplayManager>();
        this.SetInClosedDeck();
        this.bc2d = GetComponent<BoxCollider2D>();
        this.csm = GetComponent<CardStateManager>();
        this.fip = GetComponent<FlipInPlace>();
    }
}
