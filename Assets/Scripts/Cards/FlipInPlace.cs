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
        if(this.isFlipped == false &&
            this.overlapDetection.IsSpriteOnTop() == true &&
            this.cm.GetIsInClosedDeck() == false
            )
        {
            this.isFlipped = true;
            CardSymbol cs = this.cm.GetCardSymbol();
            CardValue cv = this.cm.GetCardValue();
            Sequence flipInPlaceSeq = DOTween.Sequence();
            Tween shrink = this.transform.DOScaleX(0, this.flipTime/2).OnComplete(() => displayManager.ShowCardFace((cs, cv)));
            Tween expand = this.transform.DOScaleX(this.originalScale.x, this.flipTime/2);
            flipInPlaceSeq.Append(shrink);
            flipInPlaceSeq.Append(expand);
        }
    }

    public void SetIsFlipped(bool isCardFlip)
    {
        this.isFlipped = isCardFlip;
    }

    private void Start()
    {
        this.SetIsFlipped(!cm.GetShowBackAtStart());
    }

    private void Awake()
    {
        this.displayManager = GetComponent<DisplayManager>();
        this.overlapDetection = GetComponent<OverlapDetection>();
        this.cm = GetComponent<CardManager>();
        this.originalScale = this.transform.localScale;
    }
}
