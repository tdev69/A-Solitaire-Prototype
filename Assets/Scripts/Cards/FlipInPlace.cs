using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipInPlace : MonoBehaviour
{
    [SerializeField] private float flipTime = 0.25f;
    private bool isFlipped = false;
    private CardManager cm = null;
    private DisplayManager displayManager = null;
    private OverlapDetection overlapDetection = null;
    private Vector3 originalScale = new Vector3();

    public void FlipCardOnFace()
    {
        if(isFlipped == false &&
            overlapDetection.IsSpriteOnTop() == true &&
            cm.GetIsInClosedDeck() == false
            )
        {
            isFlipped = true;
            CardSymbol cs = cm.GetCardSymbol();
            CardValue cv = cm.GetCardValue();
            Sequence flipInPlaceSeq = DOTween.Sequence();
            Tween shrink = transform.DOScaleX(0, flipTime/2).OnComplete(() => displayManager.ShowCardFace((cs, cv)));
            Tween expand = transform.DOScaleX(originalScale.x, flipTime/2);
            flipInPlaceSeq.Append(shrink);
            flipInPlaceSeq.Append(expand);
        }
    }

    public void SetIsFlipped(bool isCardFlip)
    {
        isFlipped = isCardFlip;
    }

    private void Start()
    {
        SetIsFlipped(!cm.GetShowBackAtStart());
    }

    private void Awake()
    {
        displayManager = GetComponent<DisplayManager>();
        overlapDetection = GetComponent<OverlapDetection>();
        cm = GetComponent<CardManager>();
        originalScale = transform.localScale;
    }
}
