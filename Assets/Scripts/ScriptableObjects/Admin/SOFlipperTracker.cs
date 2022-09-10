using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FlipperTracker", menuName = "SO/Admin/FlipperTracker")]
public class SOFlipperTracker : ScriptableObject
{
    private GameObject closedToOpenDeckFlipper = null;
    private CardDeckFlipper cardDeckFlipper = null;
    private Tween flipTween = null;

    /// <summary>
    /// Flips the first card of the closed deck to the top of the open deck
    /// </summary>
    public void FlipFromClosedDeckToOpen()
    {
        cardDeckFlipper.FlipToTheRight();
    }

    /// <summary>
    /// Attaches the card to the object rotating from closed deck to open deck
    /// </summary>
    /// <param name="cardToFlip">The card object to flip</param>
    public void AttachToCloseDeckFlipper(GameObject cardToFlip)
    {
        cardToFlip.transform.SetParent(closedToOpenDeckFlipper.transform);
    }


    public Tween GetFlipTween()
    {
        return flipTween;
    }


    /// <summary>
    /// Keeps track of the cardDeckFlipper component.
    /// Call it using GetCardDEckFlipper to initiate the flip once card object is attached
    /// </summary>
    /// <param name="aCardDeckFlipper">the cardDeckFlipper component</param>
    public void SetCardDeckFlipper(CardDeckFlipper aCardDeckFlipper)
    {
        cardDeckFlipper = aCardDeckFlipper;
    }

    /// <summary>
    /// Allow to set the object that will flip the card. Should be called by said object
    /// </summary>
    /// <param name="aGameObject">The object that will flip the card</param>
    public void SetClosedToOpenDeckFlipper(GameObject aGameObject)
    {
        closedToOpenDeckFlipper = aGameObject;
    }


    /// <summary>
    /// tracks the tween of the flipper
    /// </summary>
    /// <param name="aFlipPercentage">The tween of the flipper</param>
    public void SetFlipTween(Tween aFlipTween)
    {
        flipTween = aFlipTween;
    }
}
