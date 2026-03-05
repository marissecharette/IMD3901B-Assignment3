using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

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

        // Parent to transform gameObject depending on what the interactable is
        if (CompareTag("Snowball"))
        {
            transform.SetParent(player.snowballParent);
        }
        else
        {
            transform.SetParent(player.candleParent);
        }
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Throw(PlayerInteraction player, Vector3 throwDirection, float throwForce)
    {
        // Unparent
        transform.SetParent(null);

        // Re-enable physics
        rb.isKinematic = false;
        rb.useGravity = true;
        col.enabled = true;

        // Add force to throw the weapon forward
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
                ice.ApplyHeat(1.0f);
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