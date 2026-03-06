using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class Ice : NetworkBehaviour
{
    // Once hit 4 times, melted == 0 and the ice cube will despawn
    public float melted = 4f;
    private NetworkVariable<float> currentMelted = new NetworkVariable<float>();

    public GameObject snowballPrefab;
    //public PlayerInteraction vrPlayer;
    //public PlayerInteraction desktopPlayer;

    // UI
    //public UI ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (IsServer)
        {
            currentMelted.Value = melted;
        }
    }

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
