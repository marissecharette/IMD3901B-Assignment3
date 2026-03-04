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

    public void Drop(PlayerInteraction player)
    {
        // Unparent
        transform.SetParent(null);

        // Re-enable physics
        rb.isKinematic = false;
        rb.useGravity = true;
        col.enabled = true;

        player.currentCandle = null;
    }
}