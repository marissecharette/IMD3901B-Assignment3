using System.Collections;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour
{
    [Header("Freeze Settings")]
    public int freezeValue = 0;          // Current freeze counter
    public int freezeThreshold = 4;      // Freeze triggers at this many hits
    public float freezeDuration = 3f;    // Seconds frozen
    private bool isFrozen = false;

    [Header("References")]
    public PlayerInteraction playerInteraction; // Reference to your movement/interaction script
    // If you have a separate movement script, disable it when frozen

    void Start()
    {
        if (playerInteraction == null)
        {
            playerInteraction = GetComponent<PlayerInteraction>();
        }
    }

    /// <summary>
    /// Call this whenever the player is hit by a snowball
    /// </summary>
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

        // Disable player interaction / movement
        if (playerInteraction != null)
        {
            playerInteraction.enabled = false;
        }

        // Wait for freeze duration
        yield return new WaitForSeconds(freezeDuration);

        // Re-enable player
        if (playerInteraction != null)
        {
            playerInteraction.enabled = true;
        }

        isFrozen = false;
        freezeValue = 0;
        Debug.Log("Player unfrozen!");
    }
}