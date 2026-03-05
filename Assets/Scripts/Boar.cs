using System.Collections;
using UnityEngine;

public class Boar : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    // Wandering stuff
    public float speed = 1f;
    private Vector3 wanderCenter;
    private float wanderRadius;
    private Vector3 target;
    private Collider col;

    private bool isActive = false;

    // UI
    //public UI ui;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;

        currentHealth = maxHealth;
    }

    void Update()
    {

        // If the boar is not active then don't do anything
        if (!isActive)
        {
            return;
        }

        // Move toward target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // If reached target, pick a new random point as the new target
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            PickNewTarget();
        }
    }

    public void SetWanderArea(Vector3 center, float radius)
    {
        wanderCenter = center;
        wanderRadius = radius;
    }

    public void SetActive(bool active)
    {
        isActive = active;

        if (col != null)
        {
            col.enabled = active;
        }

        // Set initial wander target
        if (active)
        {
            PickNewTarget();
        }
    }

    void PickNewTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        target = wanderCenter + new Vector3(randomCircle.x, 0, randomCircle.y);
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
        // Despawns
        Destroy(gameObject);
    }
}
