using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class Ice : NetworkBehaviour
{
    // Once hit 4 times, currentMelted == 0 and the ice cube will despawn
    public NetworkVariable<float> currentMelted = new NetworkVariable<float>(4f);

    public GameObject snowballPrefab;
    //public PlayerInteraction vrPlayer;
    //public PlayerInteraction desktopPlayer;

    // Ice takes 1 heat every time a candle collides with it
    public void ApplyHeat(float heat)
    {
        if (IsServer)
        {
            currentMelted.Value -= heat;
            Debug.Log("Ice took " + heat + " heat. Melt: " + currentMelted.Value);
            
            if (currentMelted.Value <= 0)
            {
                Melt();
            }
        }
    }
    
    // Ice melts
    public void Melt()
    {
        // Find the players in the scene and for each one spawn a snowball in their right hand
        GiveSnowballsClientRpc();

        // Despawn ice cube across all clients
        NetworkObject networkObj = GetComponent<NetworkObject>();
        if (networkObj != null && networkObj.IsSpawned)
        {
            networkObj.Despawn();
        }
    }

    // Give snowballs
    [ClientRpc]
    void GiveSnowballsClientRpc()
    {
        PlayerInteraction[] players = FindObjectsOfType<PlayerInteraction>();
        foreach (PlayerInteraction player in players)
        {
            if (player != null)
            {
                player.StartCoroutine(player.RespawnSnowball());
            }
        }
    }
}
