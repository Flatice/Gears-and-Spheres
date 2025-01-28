using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    public static UIManager Instance { get; private set; }  // ENCAPSULATION

    [SerializeField]
    GameObject gameOverScreen, startMenu, pauseMenu;
    [SerializeField]
    TMP_Text scoreText, highScoreText;

    [SerializeField]
    float gameOverDelayTime = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // since UIManager is a sigleton and persists between scenes, I added this method that executes every time the scene is loaded
    // the method is called by SceneController.Start()
    public void InitializeScene()
    {
        // all menus exists at startup, they are activated and deactivated wheen needed
        gameOverScreen.SetActive(false);
        startMenu.SetActive(true);
    }

    public void StartGameAnimation()
    {
        if (startMenu != null)
            startMenu.SetActive(false);
        else
        {
            startMenu = transform.Find("Menu Start").gameObject;
            startMenu.SetActive(false);
        }
    }

    public void GameOverScreen()
    {
        Invoke("GameOverAnimation", gameOverDelayTime);
    }

    private void GameOverAnimation()
    {
        // TODO: Game over screen animation
        gameOverScreen.SetActive(true);
        scoreText.SetText("Your score: " + GameManager.Instance.score);
        highScoreText.SetText("High score: " + GameManager.Instance.highScore);
    }
}
