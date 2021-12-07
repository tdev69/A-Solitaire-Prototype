using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDeckManager : MonoBehaviour
{
    [SerializeField] GameObject cardTemplate = null; 
    [SerializeField] private Vector3 closedDeckPosition = new Vector3(-1, -4, 0); 
    private ClosedDeckUI cdui = null;  
    private GameMaster gm = null;
    private SOObjective currObjective = null;

    public void CreateCardsInClosedDeckAtStart()
    {
        this.CreateCardsInClosedDeck(this.currObjective.GetCardsInClosedDeckAtStart());
    }

    public void CreateCardsInClosedDeckInGame(int aNumber)
    {
        this.CreateCardsInClosedDeck(aNumber);
    }

    private void Awake()
    {
        this.gm = GetComponentInParent<GameMaster>();
        this.currObjective = gm.GetCurrentObjective();
        this.cdui = GetComponent<ClosedDeckUI>();
    }

    private void CreateCardsInClosedDeck(int numberToCreate)
    {
        for(int x = 0; x < numberToCreate; x++)
        {
            GameObject card = Instantiate(original: cardTemplate, parent: this.gameObject.transform);
        }

        this.cdui.UpdateDisplay();
    }
}
