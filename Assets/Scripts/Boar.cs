using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class Boar : NetworkBehaviour
{
    public NetworkVariable<float> currentHealth = new NetworkVariable<float>(10);

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

    public void Die()
    {
        NetworkObject networkObj = GetComponent<NetworkObject>();
        if (networkObj != null && networkObj.IsSpawned)
        {
            networkObj.Despawn();
        }
    }
}
