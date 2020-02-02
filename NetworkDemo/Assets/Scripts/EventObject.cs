using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TriggerEvent(ObjectMessage objectMessage)
    {
        this.GetType().GetMethod("Event" + objectMessage.EventName).Invoke(this, objectMessage.Data);
    }
}
