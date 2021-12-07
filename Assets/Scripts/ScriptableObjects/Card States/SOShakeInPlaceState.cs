using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShakeInPlaceState", menuName = "SO/CardStates/ShakeInPlaceState")]
public class SOShakeInPlaceState : SOCardState
{
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float strength = 25f;
    [SerializeField] private int vibrato = 75;
    [SerializeField] private float randomness = 100f;
    public override void ClickBehaviour(GameObject aCardGameObject, CardManager aCardManager)
    {
        aCardGameObject.transform.DOShakeRotation(this.duration, this.strength, this.vibrato, this.randomness);
    }
}
