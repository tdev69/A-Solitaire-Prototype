using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckCardCanMoveState", menuName = "SO/CardStates/CheckCardCanMove")]
public class SOCheckCardCanMoveState : SOCardState
{
    [SerializeField] SOAllCards allCards = null;
    [SerializeField] float jumpPower = 3f;
    [SerializeField] Vector3 rotateTo = new Vector3(0, 0, 1080);
    [SerializeField] SOShakeInPlaceState shakeInPlaceState;
    [Space]
    [Header("Game Events")]
    [SerializeField] SOGameEvent checkContact = null;
    [SerializeField] SOGameEvent AddToCombo = null;
    [SerializeField] SOGameEvent ResetCombo = null;

    public override void ClickBehaviour(GameObject aGameObject, CardManager aCardManager)
    {
        CardValue thisCardValue = aCardManager.GetCardValue();

        if(this.CheckIfCanMove(thisCardValue) == true)
        {
            aCardManager.MoveToOpenDeck();
            this.JumpToOpenDeck(aGameObject);
            this.AddToCombo.Raise();
            this.checkContact.Raise();
        }

        else
        {
            this.ResetCombo.Raise();
            this.shakeInPlaceState.ClickBehaviour(aGameObject, aCardManager);
        }
    }

    private bool CheckIfCanMove(CardValue clickedCardValue)
    {
        CardValue openDeckCardValue = allCards.GetCurrentOpenDeckCard().Item2;
        Debug.Log("open deck card value = " + openDeckCardValue);

        switch(clickedCardValue)
        {
            case (CardValue.ace):
                if(openDeckCardValue == CardValue.king || openDeckCardValue == CardValue.two)
                {
                    return true;
                }
                break;
            
            case (CardValue.king):
                if(openDeckCardValue == CardValue.ace || openDeckCardValue == CardValue.queen)
                {
                    return true;
                }
                break;
            
            default:
                if(openDeckCardValue + 1 == clickedCardValue || openDeckCardValue - 1 == clickedCardValue)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    private void JumpToOpenDeck(GameObject aGameObject)
    {
        Sequence jumpAndFlip = DOTween.Sequence();
        jumpAndFlip.Append(aGameObject.transform.DOJump(endValue: this.openDeckPosition,
                                                        jumpPower: this.jumpPower,
                                                        numJumps: 1,
                                                        duration: this.timeToOpenDeck
                                                        ));
        jumpAndFlip.Join(aGameObject.transform.DORotate(endValue: this.rotateTo,
                                                        duration: this.timeToOpenDeck,
                                                        mode: RotateMode.FastBeyond360
                                                        ));
        jumpAndFlip.SetEase(Ease.Linear);
    }
}
