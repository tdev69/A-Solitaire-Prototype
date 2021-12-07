using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickManager : MonoBehaviour
{
    //Verifies which card has been clicked and determines action for this card only based on state
    private void ClickAction()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(origin: mousePos2D, direction: Vector2.zero, distance: 0.001f);

        if (hit.collider != null) 
        {
            CardManager cm = hit.collider.gameObject.GetComponent<CardManager>();
            cm.TriggerClickBehaviour();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.ClickAction();
        }
    }
}
