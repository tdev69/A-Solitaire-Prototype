using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpToOpenDeckState", menuName = "SO/CardStates/JumpToOpenDeckState")]
public class SOJumpToOpenDeckState : SOCardState
{
    public override void ClickBehaviour(GameObject aGameObject, CardManager aCardManager)
    {
        aGameObject.transform.DOMove(openDeckPosition, timeToOpenDeck);
    }

}
