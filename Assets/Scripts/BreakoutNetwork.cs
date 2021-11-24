using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BreakoutNetwork: NetworkManager
{
    public Transform P1SpawnPos;
    public Transform P2SpawnPos;
    private GameObject PrefBall;

    //Fully override the code for when a new player connects to the server
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        //Add player at correct spawn position
        Transform start = numPlayers == 0 ? P1SpawnPos : P2SpawnPos;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        //Creates ball for the Player
        PrefBall = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
        NetworkServer.Spawn(PrefBall, conn);
    }

    //Adds function for when player leaves to destroy their ball too
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        //Destroys Ball
        if (PrefBall != null)
        {
            NetworkServer.Destroy(PrefBall);
        }

        //Calls base functionality
        base.OnServerDisconnect(conn);
    }

    //When the Host stops hosting destroy this since it looses refrences to the spawnpos transforms upon reloading
    //and a new one that would have the refrences is automaticly created but is blocked if this one persists
    public override void OnStopHost()
    {
        Destroy(this.gameObject);

        //Calls base functionality
        base.OnStopHost();
    }


}
