using System.Collections;
using UnityEngine;

public class Boar : MonoBehaviour
{

    public float maxHealth = 100f;
    public float currentHealth; // The UI is going to want to know this

    private bool isDead = false;

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
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Boar plays sound when hit
        //ac.PlayOneShot(hitSound);

        // If the boar is already dead then it won't take damage
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;
        Debug.Log("Boar took " + damage + "damage. Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Boar plays sound when it dies
        //ac.PlayOneShot(deathSound);
        
        isDead = true;

        // Triggers death animation
        //GetComponent<Animator>().SetTrigger("Dead");

        // For the UI
        //manager.ui.BoarKilled();
    }
}
