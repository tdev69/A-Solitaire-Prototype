using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AllCards", menuName = "SO/AllCards")]
public class SOAllCards : ScriptableObject
{
    [SerializeField] private SOCardInfo originalCardInfo = null;
    [SerializeField] private SOLevelCards cardsInLevel = null;
    private List<(CardSymbol, CardValue)> allCards = new List<(CardSymbol, CardValue)>();    //Contains information about unique cards
    private List<SOCardInfo> availableCards = new List<SOCardInfo>(); //Cards available to create a deck after level cards have been loaded
    private List<(CardSymbol, CardValue)> closedDeckCards = new List<(CardSymbol, CardValue)>(); //Cards in the deck the player can pull from
    private List<(CardSymbol, CardValue)> openDeckCards = new List<(CardSymbol, CardValue)>(); //Cards on which the player can pass level cards
    

    /// <summary>
    /// Adds a card to the top of the closed card deck
    /// </summary>
    // Index 0 will serve as the top of the deck.
    public void AddCardToClosedDeck((CardSymbol, CardValue) aCard)
    {
        this.closedDeckCards.Insert(0, aCard);
    }

    public List<(CardSymbol, CardValue)> GetCompleteClosedDeckList()
    {
        return this.closedDeckCards;
    }

    /// <summary>
    /// Returns the card at the specified index of availableCardsList and removes it from the list
    /// </summary>
    /// <param name="indexOfCard"></param>
    /// <returns></returns>
    public SOCardInfo GetCardFromAvailableCards(int indexOfCard)
    {
        SOCardInfo aCard = this.availableCards[indexOfCard];
        this.availableCards.RemoveAt(indexOfCard);
        Debug.Log("index of cards before exception = " + indexOfCard);
        return aCard;
    }

    public (CardSymbol, CardValue) GetCurrentOpenDeckCard()
    {
        return this.openDeckCards[this.openDeckCards.Count - 1];
    }

    /// <summary>
    /// Returns the index of the SOCardInfo object with the sme values as passed.
    /// If no SOCardInfo has the same values, returns -1.
    /// </summary>
    /// <param name="aCardInfo"></param>
    /// <returns></returns>
    public int GetIndexFromAvailableCards((CardSymbol, CardValue) aCardInfo)
    {
        foreach(SOCardInfo aCard in this.availableCards)
        {
            CardSymbol cs = aCard.GetCardSymbol();
            CardValue cv = aCard.GetCardValue();

            if(cs == aCardInfo.Item1 && cv == aCardInfo.Item2)
            {
                return availableCards.IndexOf(aCard);
            }
        }

        return -1;
    }

    public int GetNumberOfCardsInClosedDeck()
    {
        return this.closedDeckCards.Count;
    }

    public int GetNumberOfCardsInLevel()
    {
        return this.cardsInLevel.GetNumberOfCardsInLevel();
    }

    public int GetNumberOfCardsInOpenDeck()
    {
        return this.openDeckCards.Count;
    }

    /// <summary>
    /// Returns a random card from the list of available cards. Removes that card from the list.
    /// </summary>
    /// <param name="GetRandomCard("></param>
    /// <returns></returns>
    public SOCardInfo GetRandomCard()
    {
        if(this.availableCards.Count == 0)
        {
            this.CreateAllCards();
        }

        //int index = Random.Range(0, this.availableCards.Count);
        return this.GetCardFromAvailableCards(0);
    }

    /// <summary>
    /// Takes the card on top of the closed deck and puts it on top of the open deck. Returns the card it moved
    /// </summary>
    /// <param name="MoveFromClosedDeckToOpenDeck("></param>
    /// <returns></returns>
    public (CardSymbol, CardValue) MoveFromClosedDeckToOpenDeck()
    {
        (CardSymbol, CardValue) topCard = this.closedDeckCards[0];
        this.closedDeckCards.RemoveAt(0);
        this.PutCardInOpenDeck(topCard);
        return topCard;
    }
    private void PopulateAllCardsList()
    {
        foreach(CardSymbol cs in CardSymbol.GetValues(typeof(CardSymbol)))
        {
            foreach(CardValue cv in CardValue.GetValues(typeof(CardValue)))
            {
                this.allCards.Add((cs, cv));
            }
        }
    }

    public void PutCardInOpenDeck((CardSymbol, CardValue) aCard)
    {
        this.openDeckCards.Add(aCard);
    }

    private void CreateAllCards()
    {
        foreach((CardSymbol, CardValue) cards in allCards)
        {
            SOCardInfo cardInfo = (SOCardInfo)ScriptableObject.CreateInstance<SOCardInfo>();
            cardInfo.SetCardInformation(cards);
            this.availableCards.Add(cardInfo);
        }

        this.RandomiseAvailableCards();
    }

    private void OnEnable()
    {
        this.availableCards.Clear();
        this.PopulateAllCardsList();
        this.CreateAllCards(); //Creates all the CardInfo objects that can be passed to Card Manager
    }

    private void OnDisable()
    {
        this.availableCards.Clear();
    }

    private void RandomiseAvailableCards()
    {
        for(int i = 0; i < this.availableCards.Count; i++)
        {
            SOCardInfo firstCard = this.availableCards[i];
            int randomIndex = Random.Range(i, this.availableCards.Count);
            this.availableCards[i] = this.availableCards[randomIndex];
            this.availableCards[randomIndex] = firstCard;
        }
    }
}
