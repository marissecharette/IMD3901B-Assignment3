using System.Collections;
using UnityEngine;

public class Ice : MonoBehaviour
{
    // Once hit 4 times, melted == 0 and the ice cube will despawn
    public float melted = 4f;
    public float currentMelted;

    public GameObject snowballPrefab;
    //public PlayerInteraction vrPlayer;
    //public PlayerInteraction desktopPlayer;

    // UI
    //public UI ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMelted = melted;
    }

    public void ApplyHeat(float heat)
    {
        currentMelted -= heat;
        Debug.Log("Ice took " + heat + " heat. Melt: " + currentMelted);

        if (currentMelted <= 0)
        {
            Melt();
        }
    }

    public void Melt()
    {
        // Find the players in the scene and for each one spawn a snowball in their right hand
        PlayerInteraction[] players = FindObjectsOfType<PlayerInteraction>();

        foreach (PlayerInteraction player in players)
        {
            if (player != null)
            {
                player.StartCoroutine(player.RespawnSnowball());
            }
        }
        
        // Despawns
        Destroy(gameObject);
    }
}
