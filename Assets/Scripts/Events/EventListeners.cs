using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListeners : MonoBehaviour
{
    [SerializeField] private List<EventListener> eventsToListen = new List<EventListener>();

    private void OnEnable()
    {
        foreach(EventListener el in this.eventsToListen)
        {
            el.Register();
        }
    }

    private void OnDisable()
    {
        foreach(EventListener el in this.eventsToListen)
        {
            el.Unregister();
        }
    }
}
