using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDemoMessageSender : MonoBehaviour
{
	static NetworkDemoMessageSender _instance;

    public static NetworkDemoMessageSender getInstance()
	{
		return _instance;
	}

public void SendObjectEvent(ObjectMessage objectMessage)
	{
        EventObject[] eventObjects = GameObject.FindObjectsOfType<EventObject>();
        foreach(EventObject eventObject in eventObjects)
        {
            if(eventObject.transform.name == objectMessage.GameObjectID)
            {
                eventObject.TriggerEvent(objectMessage);
            }
        }
	}

    // Start is called before the first frame update
    void Start()
    {
		NetworkDemoMessageSender._instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            PlayerNetworkManager.getInstance().SendObjectEvent(
                new ObjectMessage("Open", "DoorA"));
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            PlayerNetworkManager.getInstance().SendObjectEvent(
                new ObjectMessage("SpawnItem", "Key", PlayerNetworkManager.getInstance().CoopPlayerID, "RoomA", "3"));
        }
    }
}
