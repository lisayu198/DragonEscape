using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private int score = 0;
    private int highScore = 0;

    void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Initialize the displays
        UpdateScoreDisplay();
        UpdateHighScoreDisplay();
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreDisplay();
        Debug.Log("Score increased. Current score: " + score);
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreDisplay();
            Debug.Log("New high score set: " + highScore);
        }
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void UpdateHighScoreDisplay()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // This method will be called when the scene is reloaded
    void OnEnable()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreDisplay();
    }
}