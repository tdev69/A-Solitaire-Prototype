using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cardStates {jumpToOpenDeck, flipToOpenDeck, shakeInPlace, doNothing, checkOpenDeckCard};

public abstract class SOCardState : ScriptableObject, ICardState
{
    [SerializeField] protected float timeToOpenDeck = 1f;
    [SerializeField] protected Vector3 openDeckPosition = new Vector3 (1, -4, 0);

    public abstract void ClickBehaviour(GameObject aGameObject, CardManager aCardManager);
}
