using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDemoEventObject : EventObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Open()
    {
        Debug.Log("You made it!");
    }
}
