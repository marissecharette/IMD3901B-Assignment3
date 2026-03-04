using System.Collections;
using UnityEngine;

public class Ice : MonoBehaviour
{

    public float melted = 4f;
    public float currentMelted;

    private bool isMelted = false;

    // Audio stuff
    //public AudioClip hitSound;
    //public AudioClip deathSound;
    //private AudioSource ac;

    // UI
    //public UI ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ac = GetComponent<AudioSource>();
        currentMelted = melted;
    }

    public void ApplyHeat(int heat)
    {
        // Ice plays sound when melted
        //ac.PlayOneShot(hitSound);

        // If the ice is already melted then it won't take more heat
        if (isMelted)
        {
            return;
        }

        currentMelted -= heat;
        Debug.Log("Ice took " + heat + "heat. Melt: " + currentMelted);

        if (currentMelted <= 0)
        {
            Melt();
        }
    }

    public void Melt()
    {
        // Ice plays sound when it melts completely
        //ac.PlayOneShot(deathSound);
        
        isMelted = true;

        // Despawns
        Destroy(gameObject);
    }
}
