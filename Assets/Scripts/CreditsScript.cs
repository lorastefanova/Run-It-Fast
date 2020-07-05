using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{

    public AudioSource buttonSound; // Audio variable

    // Function for menu button
    private IEnumerator MenuButton()
    {
        buttonSound.Play();                    // Play the sound
        yield return new WaitForSeconds(0.7f); // Wait for it to finish
        SceneManager.LoadScene("MenuScene");   // Change the scene

    }

    // Function to access MenuButton
    public void MenuSceneLoad()
    {
        StartCoroutine("MenuButton");
    }
}
