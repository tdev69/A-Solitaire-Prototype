using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStateManager : MonoBehaviour
{
    [SerializeField] private SOCardState currCardState = null;

    [Header("Possible Card States")]
    [SerializeField] private SOCardState checkCardCanMove = null;
    [SerializeField] private SOCardState flipToOpenDeck = null;
    [SerializeField] private SOCardState jumpToOpenDeck = null;
    [SerializeField] private SOCardState shakeInPlace = null;
    [SerializeField] private SOCardState doNothing = null;

    private CardManager cardManager = null;
    private OverlapDetection overlapDetection = null;

    public void SetCardState()
    {
        bool isOnTop = this.overlapDetection.IsSpriteOnTop();
        bool isInClosedDeck = this.cardManager.GetIsInClosedDeck();

        if(isOnTop == false && isInClosedDeck == false)
        {
            this.SetCurrentState(cardStates.shakeInPlace);
        }

        else if(isOnTop == true && isInClosedDeck == false)
        {
            this.SetCurrentState(cardStates.checkOpenDeckCard);
        }

        else if(isOnTop == false && isInClosedDeck == true)
        {
            this.SetCurrentState(cardStates.doNothing);
        }

        else if(isInClosedDeck == true)
        {
            this.SetCurrentState(cardStates.flipToOpenDeck);
        }
    }

    /// <summary>
    /// Sets the state of action a card should be in.
    /// The state will determine how the card reacts when clicked.
    /// </summary>
    /// <param name="aCardState">aCardState</param>
    public void SetCurrentState(cardStates aCardState)
    {
        switch(aCardState)
        {
            case cardStates.checkOpenDeckCard:
                this.currCardState = this.checkCardCanMove;
                break;

            case cardStates.flipToOpenDeck:
                this.currCardState = this.flipToOpenDeck;
                break;

            case cardStates.jumpToOpenDeck:
                this.currCardState = this.jumpToOpenDeck;
                break;

            case cardStates.shakeInPlace:
                this.currCardState = this.shakeInPlace;
                break;

            case cardStates.doNothing:
                this.currCardState = this.doNothing;
                break;
        }
    }

    public void TriggerClickBehaviour(GameObject clickedCard)
    {
        this.currCardState.ClickBehaviour(clickedCard, this.cardManager);
    }

    private void Awake()
    {
        this.cardManager = GetComponent<CardManager>();
        this.overlapDetection = GetComponent<OverlapDetection>();
    }

    private void Start()
    {
        this.SetCardState();
    }
}
