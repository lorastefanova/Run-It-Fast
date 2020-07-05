using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller; // Character controller
    public PlayerMovement playermovement;   // This script, used to disable player movement in the beginning
    public GameObject player;               // The player
    private bool isDead = false;            // Is the player dead?
    public AudioSource death;               // Audio on death
    public Text message;                    // Pop up message 
    private float transition = 6.0f; 

    public static float forwardForce = 5.0f; // Variable that determines the forward force

    

    // Start is called before the first frame update
    private void Start()
    {
        
        controller = GetComponent<CharacterController>(); // Character controller
        StartCoroutine("DisableScript");                  // Disable player movement in the beginning of the game
        StartCoroutine("Message");                        // Pop up message
        isDead = false;                                   // Player is not dead
        forwardForce = 5.0f;                              // Speed of the player
    }

    // Restricting the player movement until the camera animation is done
    private IEnumerator DisableScript()
    {
        playermovement.enabled = false;      // Disable script

        yield return new WaitForSeconds(2f); // Wait 2 seconds

        playermovement.enabled = true;       // Enable script
    }

    // Pop up message giving information about the game
    private IEnumerator Message()
    {
        yield return new WaitForSeconds(1f);                                                  // Wait 1 second
        message.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 1, 0, 1), transition); // Text fades in
        yield return new WaitForSeconds(6f);                                                  // Wait 4 seconds
        message.color = Color.Lerp(new Color(0, 1, 0, 1), new Color(0, 0, 0, 0), transition); // Text fades out
    }

    // Update is called once per frame
    private void Update()
    {
        // If the player is dead, return
        if (isDead)
        {
            return;
        }

        controller.Move((Vector3.forward * forwardForce) * Time.deltaTime); // Player moves constantly forward

        //* Tap left and right sides of the screen to move the player *//
        if ((Input.mousePosition.x > (Screen.width * 0.75f))
            && (Input.mousePosition.y <= (Screen.height * 0.5f))
            && Input.GetMouseButton(0))
        {
           
            player.transform.Translate(new Vector3(0.1f, 0, 0));
        }
        else if ((Input.mousePosition.x <= (Screen.width * 0.25f))
                && (Input.mousePosition.y <= (Screen.height * 0.5f))
                && Input.GetMouseButton(0))
        {
            
            player.transform.Translate(new Vector3(-0.1f, 0, 0));
        }
        
    }

    // Function to increase speed over time
    public void SetSpeed(float modifier)
    {
        forwardForce = 5.0f + modifier;
    }

    // Function to detect collision on the z axis
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // If the player hits something - dies
        if(hit.point.z > transform.position.z + controller.radius)
        {
            Death();
        }

    }

    // Death function
    private void Death()
    {
        isDead = true;                      // The player is dead
        GetComponent<ScoreScript>().Dead(); // Score count stops
        death.Play();                       // Sound on death play
    }

    // On trigger function to collect coins
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);                   // If collected, destroy coin
        GetComponent<ScoreScript>().CoinCollected(); // If collected, add to the score
    }
}

