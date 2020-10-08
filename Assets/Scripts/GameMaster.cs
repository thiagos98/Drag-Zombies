using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
