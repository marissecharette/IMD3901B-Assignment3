using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerInteraction : MonoBehaviour
{

    public float interactRange = 2f;
    public Camera playerCamera;

    public GameObject currentItem;
    public Transform candleParent;
    public Transform snowballParent;

    public GameObject snowballPrefab;

    public bool isVRPlayer = false;
    public Transform vrRightHandSpawn;

    public float cooldown = 1.0f;
    
    // Audio stuff

    // UI stuff
    public CrosshairUI crosshairUIScript;

    void Start()
    {
        //ac = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Crosshair status resets
        crosshairUIScript.SetInteract(false);

        // If the player presses E and is holding a candle, throw the candle
        if (Keyboard.current.eKey.wasPressedThisFrame && currentItem != null)
        {

            GameObject thrownItem = currentItem;

            Interactable item = thrownItem.GetComponent<Interactable>();

            Vector3 throwDir = playerCamera.transform.forward;
            item.Throw(this, throwDir, 30f);

            currentItem = null;

            // If the thrown item was a snowball, then start a respawn loop
            if (thrownItem.CompareTag("Snowball"))
            {
                StartCoroutine(RespawnSnowball());
            }

            return;
        }
       
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // If object is interactable, run Interact()
        if(Physics.Raycast(ray, out hit, interactRange))
        {
            if(hit.collider.CompareTag("Interactable"))
            {
                crosshairUIScript.SetInteract(true);

                if(Keyboard.current.eKey.wasPressedThisFrame)
                {
                    // Pickup candle
                    Interactable newItem = hit.collider.GetComponent<Interactable>();

                    if (newItem != null) {
                        newItem.Pickup(this);
                        //ac.PlayOneShot(pickupSound);
                    }

                }
                return;
            }
        }
    }

    // After a snowball is thrown, wait for 1s before spawning a new one in the player's hand
    public IEnumerator RespawnSnowball()
    {
        yield return new WaitForSeconds(cooldown);

        if (isVRPlayer == true)
        {
            SpawnSnowballVR();
        }
        else
        {
            SpawnSnowballDesktop();
        }
    }

    void SpawnSnowballDesktop()
    {
        GameObject newSnowball = Instantiate(snowballPrefab, snowballParent.position, snowballParent.rotation);

        Interactable interactable = newSnowball.GetComponent<Interactable>();
        interactable.Pickup(this);
    }

    void SpawnSnowballVR()
    {
        // Spawn in front of right hand
        GameObject newSnowball = Instantiate(snowballPrefab, vrRightHandSpawn.position, vrRightHandSpawn.rotation);
        NetworkObject netObj = newSnowball.GetComponent<NetworkObject>();
        netObj.Spawn();
    }
}
