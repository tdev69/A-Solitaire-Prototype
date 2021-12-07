using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoNothing", menuName = "SO/CardStates/DoNothing")]
public class SODoNothing : SOCardState
{
    public override void ClickBehaviour(GameObject anObject, CardManager aCardManager) {}
}
