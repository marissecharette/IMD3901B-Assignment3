using System.Collections;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour
{
    [Header("Freeze Settings")]
    public int freezeValue = 0;
    public int freezeThreshold = 4;
    public float freezeDuration = 3f;
    private bool isFrozen = false;

    [Header("References")]
    public PlayerInteraction playerInteraction;

    [Header("Snowball Settings")]
    public GameObject snowballPrefab;
    public Transform snowballParent;

    private bool hasSnowball = false;

    void Start()
    {
        if (playerInteraction == null)
        {
            playerInteraction = GetComponent<PlayerInteraction>();
        }
    }

    // -------------------------
    // FREEZE SYSTEM
    // -------------------------

    public void HitBySnowball()
    {
        if (isFrozen) return;

        freezeValue++;
        Debug.Log($"Player freeze: {freezeValue}/{freezeThreshold}");

        if (freezeValue >= freezeThreshold)
        {
            StartCoroutine(FreezePlayerCoroutine());
        }
    }

    private IEnumerator FreezePlayerCoroutine()
    {
        isFrozen = true;
        Debug.Log("Player frozen!");

        if (playerInteraction != null)
        {
            playerInteraction.enabled = false;
        }

        yield return new WaitForSeconds(freezeDuration);

        if (playerInteraction != null)
        {
            playerInteraction.enabled = true;
        }

        isFrozen = false;
        freezeValue = 0;
        Debug.Log("Player unfrozen!");
    }

    // -------------------------
    // SNOWBALL SYSTEM
    // -------------------------

    public void SpawnSnowball()
    {
        if (hasSnowball) return;

        GameObject snowball = Instantiate(
            snowballPrefab,
            snowballParent.position,
            Quaternion.identity,
            snowballParent
        );

        // Make it behave like the candles
        Rigidbody rb = snowball.GetComponent<Rigidbody>();
        Collider col = snowball.GetComponent<Collider>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (col != null)
        {
            col.enabled = false;
        }

        hasSnowball = true;

        // Tell PlayerInteraction the player is now holding this object
        playerInteraction.currentCandle = snowball;
    }

    public void SnowballThrown()
    {
        hasSnowball = false;
        StartCoroutine(RespawnSnowball());
    }

    private IEnumerator RespawnSnowball()
    {
        yield return new WaitForSeconds(1f);
        SpawnSnowball();
    }
}