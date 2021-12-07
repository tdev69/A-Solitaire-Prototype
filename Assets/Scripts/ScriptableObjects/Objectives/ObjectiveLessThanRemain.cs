using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LessThanObjective", menuName = "SO/Objectives/LessThanObjective")]
public class ObjectiveLessThanRemain : SOObjective
{
    [SerializeField] private int maxInLevelCardsRemaining = 0;


    private string objectiveText = "Less than {0} cards remaining";
    private string currentStatusText = "{0} cards remaining";

    public string GetCurrentStatusText()
    {
        return string.Format(currentStatusText, this.cardsInLevel.GetNumberOfCardsInLevel());
    }

    public string GetObjectiveText()
    {
        return string.Format(objectiveText, this.maxInLevelCardsRemaining);
    }

    public override bool IsObjectiveComplete()
    {
        return this.cardsInLevel.GetNumberOfCardsInLevel() <= this.maxInLevelCardsRemaining;
    }
}
