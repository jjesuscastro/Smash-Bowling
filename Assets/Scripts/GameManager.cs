using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level = 1;

    bool gameStarted;

    #region Singleton
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.LogWarning("[GameManager.cs] - Multiple GameManager(s) found!");
            Destroy(gameObject);
        }


    }
    #endregion 

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!gameStarted)
        {
            Time.timeScale = 0f;
            Debug.Log("[GameManager.cs] - Test");
            LevelController.instance.StartGame(level);
            gameStarted = true;
        }
    }

    public void NextLevel()
    {
        level = (level + 1 > 5) ? 1 : level + 1;
        SceneManager.LoadScene(0);
        gameStarted = false;
    }
}
