using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class Boar : NetworkBehaviour
{
    // Initialize health of the boar
    public NetworkVariable<float> currentHealth = new NetworkVariable<float>(10);

    // Boar takes 1 damage every time a snowball collides with it
    public void TakeDamage(float damage)
    {
        if (IsServer)
        {
            currentHealth.Value -= damage;
            Debug.Log("Boar took " + damage + " damage. Health: " + currentHealth.Value);
            
            if (currentHealth.Value <= 0)
            {
                Die();
            }
        }
    }

    // Despawn boar
    public void Die()
    {
        NetworkObject networkObj = GetComponent<NetworkObject>();
        if (networkObj != null && networkObj.IsSpawned)
        {
            networkObj.Despawn();
        }
    }
}
