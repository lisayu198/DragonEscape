using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public string gameSceneName = "MainScene"; 

    void Start()
    {
        // Mute audio at start
        AudioListener.volume = 0;
    }

    void Update()
    {
        // Check for any key press to start the game
        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        // Unmute audio when starting
        AudioListener.volume = 1;
        // Load the main game scene
        SceneManager.LoadScene("MainScene"); 
    }
}