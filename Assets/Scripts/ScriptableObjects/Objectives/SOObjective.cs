using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOObjective : ScriptableObject
{
    [SerializeField] protected int cardsInClosedDeckAtStart = 20;
    [SerializeField] protected SOLevelCards cardsInLevel = null;
    public abstract bool IsObjectiveComplete();
    
    public int GetCardsInClosedDeckAtStart()
    {
        return this.cardsInClosedDeckAtStart;
    }
}
