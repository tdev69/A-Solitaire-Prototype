using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosedDeckUI : MonoBehaviour
{
    [SerializeField] private SOAllCards allCards = null;
    private Text remainingCardsDisplay = null;

    public void UpdateDisplay()
    {
        this.remainingCardsDisplay.text = this.allCards.GetNumberOfCardsInClosedDeck().ToString();
    }

    private void Start()
    {
        this.UpdateDisplay();
    }

    private void Awake()
    {
        this.remainingCardsDisplay = GetComponentInChildren<Text>();
    }
}
