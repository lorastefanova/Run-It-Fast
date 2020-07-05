using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{
    public Text endScore;            // Text variable

    public bool isShown = false;     // Is the screen shown?
    private float transition = 0.0f; // Transition variable
    public AudioSource buttonSound;  // Audio variable

    public Image image;              // Image variable 

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false); // Toggle screen off
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (!isShown)
        {
            return;
        }

        // Screen color fades in  
        transition += Time.deltaTime;
        image.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, .5f), transition); // From transparent to semi black

    }

    // Function to toggle the screen 
    public void ToggleDeathMenu(float score)
    {

        gameObject.SetActive(true);              // Enable the screen 
        endScore.text = ((int)score).ToString(); // Show the final score on the screen 
        isShown = true;                          // The screen is on
    }

    // Functions to for play button  
    private IEnumerator PlayAgainButton()
    {
        buttonSound.Play();                    // Play the sound
        yield return new WaitForSeconds(0.7f); // Wait for it to finish
        SceneManager.LoadScene("GameScene");   // Change scene

    }
    
    // Function to access PlayAgainButton
    public void PlayAgain()
    {
        StartCoroutine("PlayAgainButton");
    }


    // Functions for menu button 
    private IEnumerator MenuButton()
    {
        buttonSound.Play();                    // Play the sound 
        yield return new WaitForSeconds(0.7f); // Wait for it to finish
        SceneManager.LoadScene("MenuScene");   // Change the scene 
       
    }

    // Function to access MenuButton
    public void GoToMenu()
    {
        StartCoroutine("MenuButton");
    }
}
