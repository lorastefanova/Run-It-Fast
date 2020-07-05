using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Text highscore;          // Text variable
    public AudioSource buttonSound; // Sound variable 

    // Start is called before the first frame update
    private void Start()
    {
        highscore.text = "Highscore: " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString(); // Display highscore on the menu screen
    }

    // Function for start button
    private IEnumerator LoadGame()
    {
        buttonSound.Play();                    // Play sound
        yield return new WaitForSeconds(0.7f); // Wait for it to finish
        SceneManager.LoadScene("GameScene");   // Change scene 
    }

    // Function for credits button
    private IEnumerator CreditsSceneLoad()
    {
        buttonSound.Play();                    // Play sound
        yield return new WaitForSeconds(0.7f); // Wait for it to finish
        SceneManager.LoadScene("CreditsScene");// Change scene 

    }

    // Function for quit button
    private IEnumerator QuitPressed()
    {
        buttonSound.Play();                    // Play sound
        yield return new WaitForSeconds(0.7f); // Wait for it to finish
        Application.Quit();                    // Change scene 

    }

    // Function to access LoadGame
    public void ButtonStart()
    {
        StartCoroutine("LoadGame");
    }

    // Function to access CreditsSceneLoad
    public void ButtonCredits()
    {
        StartCoroutine("CreditsSceneLoad");
    }

    // Function to access QuitPressed
    public void ButtonQuit()
    {
        StartCoroutine("QuitPressed");
    }

}
