using System.Collections;
using UnityEngine;

public class Ice : MonoBehaviour
{
    // Once hit 4 times, melted == 0 and the ice cube will despawn
    public float melted = 4f;
    public float currentMelted;

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
        // Despawns
        Destroy(gameObject);
    }
}
