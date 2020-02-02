using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

#pragma warning disable 618
public class NetworkManagerDemo : NetworkManager
{
    static NetworkManagerDemo _instance;

    static public NetworkManagerDemo getInstance()
    {
        return _instance;
    }

    public List<PlayerNetworkManager> readyPlayers;

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        Debug.Log("hello no - " + conn.connectionId);
    }

    /**
     * When a client connected to this host disconnects
     */
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        int playerIndex = readyPlayers.FindIndex(x => x.PlayerNetworkID == conn.connectionId);
        readyPlayers.RemoveAt(playerIndex);
    }

    /**
     * When a client loses connection
     */
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        SceneManager.LoadScene("NetworkDemoScene");
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        readyPlayers.Clear();
        SceneManager.LoadScene("NetworkDemoScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        readyPlayers = new List<PlayerNetworkManager>();
        NetworkManagerDemo._instance = this;
    }

    public void StartGame()
    {
        ServerChangeScene("NetworkDemoScene3");
    }

    public void MarkAsNotReady(PlayerNetworkManager notReadyingPlayer, NetworkConnection client)
    {
        readyPlayers.Remove(notReadyingPlayer);
        Debug.Log("Ready Players: " + readyPlayers.Count);
        notReadyingPlayer.SetPlayerNetworkID(-1);
    }

    public void MarkAsReady(PlayerNetworkManager readyingPlayer, NetworkConnection client)
    {
        readyPlayers.Add(readyingPlayer);
        Debug.Log("Ready Players: " + readyPlayers.Count);
        readyingPlayer.SetPlayerNetworkID(client.connectionId);

        if(readyPlayers.Count == 2)
        {
            StartGame();
        }
    }

}
#pragma warning restore 618
