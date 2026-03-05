using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    public float interactRange = 2f;
    public Camera playerCamera;

    public GameObject currentCandle;
    public Transform candleParent;
    // Attack stuff
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
        if (Keyboard.current.eKey.wasPressedThisFrame && currentCandle != null)
        {
            Interactable candle = currentCandle.GetComponent<Interactable>();

            Vector3 throwDir = playerCamera.transform.forward;
            candle.Throw(this, throwDir, 30f);

            currentCandle = null;
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
                    Interactable newCandle = hit.collider.GetComponent<Interactable>();

                    if (newCandle != null) {
                        newCandle.Pickup(this);
                        //ac.PlayOneShot(pickupSound);
                    }

                }
                return;
            }
        }
    }
}
