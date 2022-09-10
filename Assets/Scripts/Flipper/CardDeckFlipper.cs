using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckFlipper : MonoBehaviour
{
    [SerializeField] private float rotationTime = 0.5f;
    [SerializeField] private SOFlipperTracker flipperTracker = null;


    /// <summary>
    /// Rotates 180° from left to right. Use when card is attached as child
    /// to bring it to open deck
    /// </summary>
    public void FlipToTheRight()
    {
        transform.DORotate(new Vector3(0, 180, 0), rotationTime).OnComplete(() => ResetRotation());
    }

    private void Awake()
    {
        flipperTracker.SetClosedToOpenDeckFlipper(transform.gameObject);
        flipperTracker.SetCardDeckFlipper(this);
    }


    /// <summary>
    /// Detaches any card attached and sets the rotation back to 0 so flipper can be reused.
    /// </summary>
    private void ResetRotation()
    {
        transform.DetachChildren();
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

}
