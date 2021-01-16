using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class GameMaster : MonoBehaviour
{
    public GameObject restartPanel;
    public Text score;
    public float timer;
    private float oldTimer;
    bool isRunning = true;
    bool isOver = false;
    public string nextLevel;
    
    public void Start()
    {
        oldTimer = timer;
        Analytics.CustomEvent("StartLevel", new Dictionary<string, object>{
            {"levelNumber", SceneManager.GetActiveScene()}
        });
    }
    public void Update()
    {
        if(!isOver)
        {
            if(timer < 0)
            {
                isRunning = false;
            }
            if(isRunning)
            {
                timer -= Time.deltaTime;
                score.text = "Tempo: " + timer;
            }
            else
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
    public void GameOver()
    {
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>{
            { "tempo", timer },
            { "level", SceneManager.GetActiveScene()}
        });
        isOver = true;
        Delay();
    }
    public void Delay()
    {
        restartPanel.SetActive(true);
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
