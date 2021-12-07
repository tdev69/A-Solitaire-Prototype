using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "CardSpritesMap", menuName = "SO/CardSpriteMap")]
public class SOSpriteMap : ScriptableObject
{
    [SerializeField] private int cardBackIndex = 27;
    [SerializeField] private Sprite[] spriteArray = null;
    private Dictionary<(CardSymbol, CardValue), int> cardSpritesMap = new Dictionary<(CardSymbol, CardValue), int>();
    private RangeInt heartIndex = new RangeInt(0, 13);
    private RangeInt diamondIndex = new RangeInt(14, 13);
    private RangeInt cloverIndex = new RangeInt(28, 13);
    private RangeInt spadesIndex = new RangeInt(42, 13);
    

    private void FillCardSpritesMap()
    {
        foreach(CardSymbol cs in CardSymbol.GetValues(typeof(CardSymbol)))
        {
            int indexClover = this.cloverIndex.start; 
            int indexDiamond = this.diamondIndex.start;
            int indexHeart = this.heartIndex.start;
            int indexSpades = this.spadesIndex.start;
            
            foreach(CardValue cv in CardValue.GetValues(typeof(CardValue)))
            {
                switch(cs) 
                {
                    case CardSymbol.clover:
                        cardSpritesMap.Add((cs, cv), indexClover);
                        indexClover += 1;
                        break;
                    
                    case CardSymbol.diamond:
                        cardSpritesMap.Add((cs, cv), indexDiamond);
                        indexDiamond += 1;
                        break;

                    case CardSymbol.heart:
                        cardSpritesMap.Add((cs, cv), indexHeart);
                        indexHeart += 1;
                        break;

                    case CardSymbol.spades:
                        cardSpritesMap.Add((cs, cv), indexSpades);
                        indexSpades += 1;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Returns the sprite used as back of card
    /// </summary>
    /// <returns></returns>
    public Sprite GetCardBackSprite()
    {
        return this.spriteArray[this.cardBackIndex];
    }

    /// <summary>
    /// Will return an int based on the information passed in the tuple aCardInfo
    /// The int is the sprite index.
    /// </summary>
    /// <param name="aCardInfo"></param>
    /// <returns></returns>
    public int GetSpriteIndex((CardSymbol, CardValue) aCardInfo)
    {
        return this.cardSpritesMap[aCardInfo];
    }

    public Sprite GetSprite(int aSpriteIndex)
    {
        return this.spriteArray[aSpriteIndex];
    }

    private void OnEnable()
    {
        this.FillCardSpritesMap();
        Debug.Log("sprite array size = " + this.spriteArray.Length);
    }
}
