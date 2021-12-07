using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventListener
{
    public SOGameEvent Event;
    public UnityEvent Response;

    public void OnEventRaised()
    {
        Response.Invoke();
    }

    public void Register()
    {
        Event.RegisterListener(this);
    }

    public void Unregister()
    {
        Event.UnregisterListener(this);
    }
}
