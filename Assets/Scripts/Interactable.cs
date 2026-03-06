using UnityEngine;
using Unity.Netcode;

public class Interactable : NetworkBehaviour
{
    private Rigidbody rb;
    private Collider col;

    public Transform candleParent;
    public Transform snowballParent;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Pickup(PlayerInteraction player)
    {
        // Assign to current candle
        player.currentItem = gameObject;

        // Fixes physics
        rb.isKinematic = true;
        rb.useGravity = false;
        col.enabled = false;

        FollowParent follow = gameObject.GetComponent<FollowParent>();
        if (follow == null)
        {
            follow = gameObject.AddComponent<FollowParent>();
        }

        // Parent to transform gameObject depending on what the interactable is
        if (CompareTag("Snowball"))
        {
            follow.StartFollowing(player.snowballParent.transform);
            follow.localOffset = new Vector3(0f, 0.2f, 0.5f);
        }
        else
        {
            follow.StartFollowing(player.candleParent.transform);
            follow.localOffset = new Vector3(3f, -1f, 1f);
        }
    }

    public void Throw(PlayerInteraction player, Vector3 throwDirection, float throwForce)
    {
        // Stop following the parent
        FollowParent follow = GetComponent<FollowParent>();
        if (follow != null)
            follow.StopFollowing();

        // Enable physics
        transform.position = transform.position; // optional, ensures no snapping
        rb.isKinematic = false;
        rb.useGravity = true;
        col.enabled = true;

        rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

        player.currentItem = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object the candle is colliding with is the ice cube, if yes then it takes a point of heat
        if (collision.gameObject.CompareTag("Ice"))
        {
            // Call ApplyHeat() on ice cube
            Ice ice = collision.gameObject.GetComponent<Ice>();
            if (ice != null)
            {
                // 1.0f so 4 candles have to hit the ice cube for it to despawn
                ice.ApplyHeat(1f);
            }
            // Despawn candle
            Destroy(gameObject);
        }

        // If the item hit the boar, then the boar takes 1 point of damage
        if (collision.gameObject.CompareTag("Boar"))
        {
            // Call TakeDamage() on the boar
            Boar boar = collision.gameObject.GetComponent<Boar>();
            if (boar != null)
            {
                boar.TakeDamage(1);
            }
            // Despawn snowball
            Destroy(gameObject);
        }
    }
}