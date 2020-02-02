using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#pragma warning disable 618
public class PlayerNetworkManager : NetworkBehaviour
{
    static PlayerNetworkManager _instance;

    public static PlayerNetworkManager getInstance()
    {
        return _instance;
    }

    [SyncVar]
    public int PlayerNetworkID;

    public int CoopPlayerID;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        PlayerNetworkID = -1;
        Debug.Log(isLocalPlayer);

        if(isLocalPlayer)
        {
            PlayerNetworkManager._instance = this;
        }
        SceneManager.LoadScene(1);
    }

    public void SetPlayerNetworkID(int newID)
    {
        PlayerNetworkID = newID;
        transform.name = "PlayerNetwork - " + PlayerNetworkID;

        // Hack, if 1 then 0
        CoopPlayerID = PlayerNetworkID == 0 ? 1 : 0;
    }

    [Command]
    private void CmdSendStringMessage(string MessageToSend)
    {
        Debug.Log(MessageToSend);
    }

    public void SendStringMessage(string MessageToSend)
    {
        CmdSendStringMessage(MessageToSend + " " + PlayerNetworkID);
    }

    [ClientRpc]
    private void RpcSendObjectEvent(int sender, string objectID, string eventName, string Data)
	{
        NetworkDemoMessageSender.getInstance().SendObjectEvent(new ObjectMessage(objectID, eventName, Data.Split(',')));
		Debug.Log("EventName: " + eventName + " | Object ID: " + objectID + " | Sender: " + sender);
    }

    [TargetRpc]
    private void TargetSendObjectEvent(NetworkConnection conn, int sender, string objectID, string eventName, string Data)
    {
        NetworkDemoMessageSender.getInstance().SendObjectEvent(new ObjectMessage(objectID, eventName, PlayerNetworkID, Data.Split(',')));
        Debug.Log("EventName: " + eventName + " | Object ID: " + objectID + " | Data: " + Data);
    }

    [Command]
    private void CmdSendObjectEvent(int sender, string objectID, string eventName, int targetPlayerID, string Data)
    {
        if (targetPlayerID != -1)
        {
            foreach (NetworkConnection networkConnection in NetworkServer.connections)
            {
                if (networkConnection.connectionId == targetPlayerID)
                {
                    TargetSendObjectEvent(networkConnection, sender, objectID, eventName, Data);
                    break;
                }
            }
        } else
        {
            RpcSendObjectEvent(sender, objectID, eventName, Data);
        }
    }

    public void SendObjectEvent(ObjectMessage ObjectEventToSend)
    {
        CmdSendObjectEvent(PlayerNetworkID, ObjectEventToSend.GameObjectID, ObjectEventToSend.EventName, ObjectEventToSend.TargetClient, string.Join(",", ObjectEventToSend.Data));
    }

    [Command]
    private void CmdSetAsReady()
    {
        NetworkManagerDemo.getInstance().MarkAsReady(this, connectionToClient);
    }
    
    public void SetAsReady()
    {
        CmdSetAsReady();
    }

    [Command]
    private void CmdSetAsNotReady()
    {
        NetworkManagerDemo.getInstance().MarkAsNotReady(this, connectionToClient);
    }

    public void SetAsNotReady()
    {
        CmdSetAsNotReady();
    }
}
#pragma warning restore 618