using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] List<(CardSymbol, CardValue)> cardInfoList = new List<(CardSymbol, CardValue)> ();
    [SerializeField] private int streakLengthToCombo = 2;
    [SerializeField] private int extraCardsForCombo = 3;
    [Space]
    [Header("Game Events")]
    [SerializeField] private SOGameEvent addCardClosedDeck = null;
    [SerializeField] private SOGameEvent createClosedDeckOfCard = null;
    [SerializeField] private SOGameEvent getCardInfo = null;
    [SerializeField] private SOGameEvent removeExistingCards = null;
    [Space]
    [Header("Systems SO")]
    [SerializeField] private SOObjective levelObjective = null;
    [SerializeField] private SOAllCards allCards = null;

    private int currentComboStreak = 0;

    public void AddOneToComboStreak()
    {
        this.currentComboStreak += 1;
        this.CheckForCombo();
    }

    public void CheckObjectiveStatus()
    {
        if(this.levelObjective.IsObjectiveComplete() == true)
        {
            //print("Objective is complete");
        }

        else
        {
            //print("objective is not yet reached");
        }
    }

    public SOObjective GetCurrentObjective()
    {
        return this.levelObjective;
    }

    public void ResetCurrentComboStreak()
    {
        this.currentComboStreak = 0;
    }

    private void CheckForCombo()
    {
        if(this.currentComboStreak >= this.streakLengthToCombo)
        {
            this.ResetCurrentComboStreak();
            this.addCardClosedDeck.Raise();
        }
    }

    private void Start()
    {
        this.removeExistingCards.Raise();
        this.getCardInfo.Raise();
        this.createClosedDeckOfCard.Raise();
    }
}
