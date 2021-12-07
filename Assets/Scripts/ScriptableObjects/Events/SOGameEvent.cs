using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "SO/Events/Game Event")]
public class SOGameEvent : ScriptableObject
{
    private List<EventListener> listeners = new List<EventListener>();

    public void Raise()
    {
        for(int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(EventListener listener)
    {
        this.listeners.Add(listener);
    }

    public void UnregisterListener(EventListener listener)
    {
        this.listeners.Remove(listener);
    }

    private void OnDisable()
    {
        listeners.Clear();
    }
}
