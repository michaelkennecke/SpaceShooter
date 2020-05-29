using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame(string level)
    {
        Score.Reset();
        SceneManager.LoadScene(level);
    }

    public void ToLevelSelectionMenu()
    {
        SceneManager.LoadScene("Menu_LevelSelection");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu_Main");
    }

    public void ToEndMenu()
    {
        SceneManager.LoadScene("Menu_End");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
