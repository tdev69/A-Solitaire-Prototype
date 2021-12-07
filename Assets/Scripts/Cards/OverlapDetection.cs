using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapDetection : MonoBehaviour
{
    private Collider2D thisCollider = null;
    private int orderInLayer = -1;
    private SpriteRenderer sr = null;

    public bool IsSpriteOnTop()
    {
        List<Collider2D> cardsInContact = this.GetContacts();
        
        if(cardsInContact.Count > 0)
        {
            foreach(Collider2D coll in cardsInContact)
            {
                int so = coll.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                
                if(orderInLayer < so)
                {
                    return false;
                }
            }
        }

        return true;
    }

    //A test to make sure that negative layer orders are never used. 
    //We use -1 to set an empty layer order. 
    private void CheckSpriteOrderInLayer()
    {
        if(this.orderInLayer < 0)
        {
            Debug.LogAssertion(this.transform.name + ": Do not use negative numbers for Order in Layer in Sprite Renderer");
        }
    }

    //Grabs all cards overlapping with this one and determine if card can move or not
    private List<Collider2D> GetContacts()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D aFilter = new ContactFilter2D().NoFilter();
        this.thisCollider.OverlapCollider(aFilter, colliders);
        
        return colliders;
    }

    private void Start()
    {
        this.orderInLayer = sr.sortingOrder;
        this.GetContacts();
        this.CheckSpriteOrderInLayer();
    }

    private void Awake()
    {
        this.thisCollider = GetComponent<BoxCollider2D>();
        this.sr = GetComponent<SpriteRenderer>();
        
    }
}
