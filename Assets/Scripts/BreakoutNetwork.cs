using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BreakoutNetwork: NetworkManager
{
    public Transform P1SpawnPos;
    public Transform P2SpawnPos;
    GameObject PrefBall;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? P1SpawnPos : P2SpawnPos;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);


        PrefBall = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));

        NetworkServer.Spawn(PrefBall, conn);


    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (PrefBall != null)
        {
            NetworkServer.Destroy(PrefBall);
        }

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
