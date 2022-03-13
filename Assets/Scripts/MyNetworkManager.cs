using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        var player = conn.identity.GetComponent<MyNetworkPlayer>();
        player.SetDisplayName($"player{numPlayers}");
        player.SetDisplayColor(new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f)));
    }
}
