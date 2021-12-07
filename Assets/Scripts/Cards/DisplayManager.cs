using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] private SOSpriteMap spriteMap = null;
    SpriteRenderer spriteRenderer = null;
    CardManager cardManager = null;

    public int GetOrderInLayer()
    {
        return this.spriteRenderer.sortingOrder;
    }
    
    public void ShowCardBack()
    {
        this.spriteRenderer.sprite = this.spriteMap.GetCardBackSprite();
    }

    public void ShowCardFace((CardSymbol, CardValue) aCardInfo)
    {
        int spriteIndex = this.spriteMap.GetSpriteIndex(aCardInfo);
        this.spriteRenderer.sprite = this.spriteMap.GetSprite(spriteIndex);
    }

    public void SetOrderInLayer(int aLayer)
    {
        this.spriteRenderer.sortingOrder = aLayer;
    }

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        //StartCoroutine(SpriteTest());
        this.cardManager = GetComponent<CardManager>();
    }

    private void Start()
    {
        this.SetZLayer();
    }

    //Puts overlapping cards on a different z coordinate, to avoid having wrong cards clicked
    private void SetZLayer()
    {
        int displayIndex = this.GetOrderInLayer();
        float zValue = (float)(0 - displayIndex * 0.01);
        Vector3 currentPos = this.transform.position;
        currentPos.z = zValue;
        this.transform.position = currentPos;
    }

    IEnumerator SpriteTest()
    {
        yield return new WaitWhile(() => this.spriteRenderer == null);
        //print("sprite renderer is finally loaded"); 
    }
}
