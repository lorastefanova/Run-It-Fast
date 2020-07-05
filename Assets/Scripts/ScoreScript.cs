using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private float score = 0.0f;       // Variable for the score
    public Text scoreText;            // Reference to the text game object
    public DeathMenuScript deathMenu; // Reference to the DeathMenu script
    public Animator anim;             // Variable for the animator

    private int difficulty = 1;       // Variable to increase the speed over time 
    private int maxDifficulty = 100;  // The maximum speed difficulty 
    private int scoreToNext = 10;     // Variable to store when is the next difficulty 

    private bool isDead = false;      // Is the player dead?

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();  // Animator
    }

    // Update is called once per frame
    private void Update()
    {
        // The player is dead play death animation
        if (isDead)
        {
            anim.SetTrigger("Death");
            return;
        }

        // If statement to increase speed over time (increased with score)
        if (score >= scoreToNext)
        {
            IncreaseSpeed();
        }

        score += Time.deltaTime * difficulty;     // 
        scoreText.text = ((int)score).ToString(); // Print the score to the screen 
    }

    // Function to increase the speed 
    private void IncreaseSpeed()
    {
        // Stop increasing if it's maximum speed
        if (difficulty == maxDifficulty)
        {
            return;
        }

        scoreToNext *= 2;  // Increase the speed on 10, 20, 40, 80, 160 etc..
        difficulty++;      // Add one to the difficulty

        GetComponent<PlayerMovement>().SetSpeed(difficulty); // Modify the speed
        
    }

    // Function to save the score of the dead player
    public void Dead()
    {
        isDead = true;                                 // Player is dead

        if (PlayerPrefs.GetFloat("Highscore") < score) // If the player beats the last highscore - set as the new 
        {
            PlayerPrefs.SetFloat("Highscore", score);
        }

        deathMenu.ToggleDeathMenu(score);              // Death menu pops up

    }

    // Function to add to the score when a coin is collected
    public void CoinCollected()
    {
        score += 10.0f;
    }
}
