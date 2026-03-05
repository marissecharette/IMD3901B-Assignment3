using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Pickup(PlayerInteraction player)
    {
        // Assign to current candle
        player.currentCandle = gameObject;

        // Fixes physics
        rb.isKinematic = true;
        rb.useGravity = false;
        col.enabled = false;

        // Parent
        transform.SetParent(player.candleParent);
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

        player.currentCandle = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object being collided with is the ice cube
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

        // Check if the object being collided with is the boar
        // Since all candles in the scene will be despawned to melt the ice,
        // only snowballs will be hitting the boar so there shouldn't be a need to differentiate
        else if (collision.gameObject.CompareTag("Boar"))
        {
            Boar boar = collision.gameObject.GetComponent<Boar>();
            if (boar != null)
            {
                // Each snowball deals 10 damage
                boar.TakeDamage(10);
            }

            // Despawn snowball
            Destroy(gameObject);
        }
    }
}