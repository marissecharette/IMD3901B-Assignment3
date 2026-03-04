using UnityEngine;

public class Interactable : MonoBehaviour
{
        // Pickup candle
        public void Pickup(PlayerInteraction player)
    {
        // If player is already holding a candle, do nothing
        if (player.currentCandle != null)
        {
            return;
        }

        player.currentCandle = gameObject;

        // Fixes physics
        player.currentCandle.GetComponent<Rigidbody>().isKinematic = true;

        player.currentCandle.transform.position = player.candleParent.transform.position;
        player.currentCandle.transform.rotation = player.candleParent.transform.rotation;

        // Set parent to candleParent game object
        player.currentCandle.transform.SetParent(player.candleParent);

        Debug.Log("Interacted with " + gameObject.name);

    }

    public void Drop(PlayerInteraction player)
    {
        // Detaches currently held candle from the player's hand
        player.candleParent.DetachChildren();

        player.currentCandle.transform.eulerAngles = new Vector3(player.currentCandle.transform.position.x, player.currentCandle.transform.position.z, player.currentCandle.transform.position.y);

        // Fixes physics
        player.currentCandle.GetComponent<Rigidbody>().isKinematic = false;
        player.currentCandle.GetComponent<BoxCollider>().isTrigger = false;
    }
}
