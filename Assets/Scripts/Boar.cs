using System.Collections;
using UnityEngine;

public class Boar : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private bool isDead = false;

    // UI
    //public UI ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boar took " + damage + " damage. Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
