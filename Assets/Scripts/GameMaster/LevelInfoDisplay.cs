using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoDisplay : MonoBehaviour
{
    [SerializeField] private SOLevelCards levelCards = null;
    [SerializeField] private SOObjective currentObjective = null;

    private Text levelUiDisplay = null;

    public void UpdateRemainingCardsInLevelDisplay()
    {
        string displayText = string.Format("{0} cards remaining", this.levelCards.GetNumberOfCardsInLevel());
        this.levelUiDisplay.text = displayText;
    }

    private void Awake()
    {
        this.levelUiDisplay = GetComponentInChildren<Text>();
    }
}
