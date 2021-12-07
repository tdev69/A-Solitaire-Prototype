using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards In Level", menuName = "SO/Objectives/Cards In Level")]
public class SOLevelCards : ScriptableObject
{
    private List<SOCardInfo> cardsInLevelInfo = new List<SOCardInfo>();
    

    /// <summary>
    /// Returns a copy of the list of Card Info of cards in level
    /// in the array passed as argument
    /// </summary>
    /// <param name="anArrayOfCardInfo"></param>
    /// <returns></returns>
    public SOCardInfo[] GetListCardsInLevel(SOCardInfo[] anArrayOfCardInfo)
    {
        this.cardsInLevelInfo.CopyTo(anArrayOfCardInfo);
        return anArrayOfCardInfo;
    }

    public int GetNumberOfCardsInLevel()
    {
        return this.cardsInLevelInfo.Count;
    }

    public void RemoveCardFromInLevel(SOCardInfo someCardInfo)
    {
        this.cardsInLevelInfo.Remove(someCardInfo);
    }

    public void SetCardInLevel(SOCardInfo someCardInfo)
    {
        this.cardsInLevelInfo.Add(someCardInfo);
    }

    private void OnEnable()
    {
        this.cardsInLevelInfo.Clear();
    }

    private void OnDisable()
    {
        this.cardsInLevelInfo.Clear();
    }
}
