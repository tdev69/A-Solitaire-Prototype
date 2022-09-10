using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlipToOpenDeck", menuName = "SO/CardStates/FlipToOpenDeck")]
public class SOFlipToOpenDeck : SOCardState
{
    [SerializeField] private SOGameEvent resetCombo = null;
    [SerializeField] private SOFlipperTracker flipperTracker = null;

    public override void ClickBehaviour(GameObject aCardGameObject, CardManager aCardManager)
    {
        flipperTracker.AttachToCloseDeckFlipper(aCardGameObject);
        flipperTracker.FlipFromClosedDeckToOpen();
        resetCombo.Raise();
    }
}
