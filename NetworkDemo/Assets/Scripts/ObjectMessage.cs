using System;
using UnityEngine;
public class ObjectMessage
{
    // Event name which translates 
    public string EventName;
    // The affected object name
    public string GameObjectID;
    // Send to a specific connection ID
    public int TargetClient;
    public string[] Data;

    public ObjectMessage()
    {

    }

public ObjectMessage(string _eventName, string _gameObjectID, params string[] _data)
    {
        EventName = _eventName;
        GameObjectID = _gameObjectID;
        TargetClient = -1;
        Data = _data;
    }

    public ObjectMessage(string _eventName, string _gameObjectID, int _targetClient, params string[] _data)
    {
        EventName = _eventName;
        GameObjectID = _gameObjectID;
        TargetClient = _targetClient;
        Data = _data;
    }
}
