using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnClientConnect()
    {
         base.OnClientConnect();
         //NetworkClient.connection
         Debug.Log("sa");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        Debug.Log("server ekledi " + numPlayers);
    }
}
