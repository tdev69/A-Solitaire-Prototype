using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSymbol {clover, diamond, heart, spades};
public enum CardColour {red, black};
public enum CardValue {ace, two, three, four, five, six, seven, eight, nine, ten, jack, queen, king};

[CreateAssetMenu(menuName = "SO/CardInfo", fileName = "CardInfoContainer")]
public class SOCardInfo : ScriptableObject
{
    [Header("Randomise = true sets Value + Symbol at run time")]
    [SerializeField] private bool randomise = true;
    [SerializeField] private CardSymbol cardSymbol = CardSymbol.clover;
    [SerializeField] private CardColour cardColour = CardColour.black;
    [SerializeField] private CardValue cardValue = CardValue.ace;
    
    private void SetCardColour()
    {
        switch(this.cardSymbol)
        {
            case CardSymbol.clover:
            case CardSymbol.spades:
                this.cardColour = CardColour.black;
            break;
            
            case CardSymbol.diamond:
            case CardSymbol.heart:
                this.cardColour = CardColour.red;
            break;
        }
    }

    public void SetCardInformation((CardSymbol, CardValue) cardInfo)
    {
        this.cardSymbol = cardInfo.Item1;
        this.cardValue = cardInfo.Item2;
        this.SetCardColour();
    }
    
    public CardSymbol GetCardSymbol() 
    {
        return this.cardSymbol;
    }

    public CardColour GetCardColour()
    {
        return this.cardColour;
    }

    public CardValue GetCardValue()
    {
        return this.cardValue;
    }
}
