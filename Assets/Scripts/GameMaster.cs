using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private Text score;
    [SerializeField] private float timer = 30f;
    [SerializeField] private string nextLevel = "Game1";

    private bool isRunning = true;
    private bool isOver;

    private void Start()
    {
        Analytics.CustomEvent("StartLevel", new Dictionary<string, object>
        {
            { "levelName", SceneManager.GetActiveScene().name }
        });
    }

    private void Update()
    {
        if (isOver)
        {
            return;
        }

        if (timer <= 0f)
        {
            isRunning = false;
        }

        if (isRunning)
        {
            timer -= Time.deltaTime;

            if (score != null)
            {
                score.text = "Tempo: " + Math.Round(timer, 2);
            }
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    public void GameOver()
    {
        if (isOver)
        {
            return;
        }

        isOver = true;
        isRunning = false;

        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            { "tempo", timer },
            { "level", SceneManager.GetActiveScene().name }
        });

        ShowRestartPanel();
    }

    public bool IsGameOver => isOver;

    private void ShowRestartPanel()
    {
        if (restartPanel != null)
        {
            restartPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
