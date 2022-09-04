using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDeckManager : MonoBehaviour
{
    [SerializeField] GameObject cardFlipper = null;
    [SerializeField] GameObject cardTemplate = null; 
    [SerializeField] private Vector3 closedDeckPosition = new Vector3(-1, -4, 0); 
    private ClosedDeckUI cdui = null;  
    private GameMaster gm = null;
    private SOObjective currObjective = null;

    public void CreateCardsInClosedDeckAtStart()
    {
        CreateCardsInClosedDeck(currObjective.GetCardsInClosedDeckAtStart());
    }

    public void CreateCardsInClosedDeckInGame(int aNumber)
    {
        CreateCardsInClosedDeck(aNumber);
    }

    public GameObject GetCardFlipper()
    {
        return cardFlipper;
    }

    public GameObject GetCardOnTop()
    {
        Transform[] closedDeckCards = transform.GetComponentsInChildren<Transform>();
        return closedDeckCards[closedDeckCards.Length - 1].gameObject;
    }

    private void Awake()
    {
        gm = GetComponentInParent<GameMaster>();
        currObjective = gm.GetCurrentObjective();
        cdui = GetComponent<ClosedDeckUI>();
    }

    private void CreateCardsInClosedDeck(int numberToCreate)
    {
        for(int x = 0; x < numberToCreate; x++)
        {
            GameObject card = Instantiate(original: cardTemplate, parent: gameObject.transform);
        }

        cdui.UpdateDisplay();
    }
}
