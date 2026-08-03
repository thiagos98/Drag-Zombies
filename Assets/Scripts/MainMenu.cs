using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game1";

    public void GoToGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
