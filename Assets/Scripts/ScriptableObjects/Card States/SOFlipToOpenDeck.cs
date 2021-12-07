using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlipToOpenDeck", menuName = "SO/CardStates/FlipToOpenDeck")]
public class SOFlipToOpenDeck : SOCardState
{
    [SerializeField] private SOGameEvent checkContactEvent = null;
    [SerializeField] private SOGameEvent resetCombo = null;
    [SerializeField] private float moveDuration = 0.5f;

    public override void ClickBehaviour(GameObject aCardGameObject, CardManager aCardManager)
    {
        float thirdMoveTime = this.moveDuration/3;
        float twoThirdMoveTime = thirdMoveTime * 2;  
        Sequence flipSequence = DOTween.Sequence().OnComplete(() => SendMessage(this.checkContactEvent));
        Sequence cardSizeSequence = DOTween.Sequence();

        Tween move = aCardGameObject.transform.DOMove(this.openDeckPosition, this.moveDuration);
        Tween shrink = aCardGameObject.transform.DOScaleX(0, thirdMoveTime).OnComplete(() => SpriteChange(aCardManager)); 
        Tween expand = aCardGameObject.transform.DOScaleX(2, twoThirdMoveTime);
        
        cardSizeSequence.Append(shrink);
        cardSizeSequence.Append(expand);
        flipSequence.Append(move);
        flipSequence.Join(cardSizeSequence);

        this.SendMessage(this.resetCombo);
    }

    private void SpriteChange(CardManager aCardManager)
    {
        aCardManager.MoveToOpenDeck();
    }

    private void SendMessage(SOGameEvent anEvent)
    {
        anEvent.Raise();
    }
}
