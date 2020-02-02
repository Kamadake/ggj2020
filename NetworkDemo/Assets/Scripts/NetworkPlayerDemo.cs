using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618
public class NetworkPlayerDemo : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!isServer)
        {
            CmdSendName("Hello");
        }
    }

    [Command]
    void CmdSendRandomMessage()
    {
        Debug.Log("I am carrot");
    }

    [ClientRpc]
    void RpcSendRandomMessage()
    {
        Debug.Log("I am potato");
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RpcSendRandomMessage();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            CmdSendRandomMessage();
        }
    }

    [Command]
    void CmdSendName(string name)
    {
        transform.name = name;
    }
}
#pragma warning restore 618