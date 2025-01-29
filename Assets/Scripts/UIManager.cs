using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    public static UIManager Instance { get; private set; }  // ENCAPSULATION

    [SerializeField]
    GameObject gameMenu, startMenu, pauseResumeButton;
    [SerializeField]
    TMP_Text menuTitle, scoreText, highScoreText;

    Image buttonSprite;

    [SerializeField]
    float delayTime = 1f;

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
        gameMenu.SetActive(false);
        startMenu.SetActive(true);
        pauseResumeButton.SetActive(false);
        
    }

    public void StartGameAnimation()
    {
        startMenu.SetActive(false);
        pauseResumeButton.SetActive(true);
    }

    public void PauseMenu()
    {
        StartCoroutine(GameMenu("PAUSE", 0));
        pauseResumeButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().SetText("R");
        // TODO: change sprite to resume sprite and deleate this line ^^^
    }

    public void HideGameMenu()
    {
        gameMenu.SetActive(false);
        pauseResumeButton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().SetText("P");
        // TODO: change sprite to pause sprite and deleate this line ^^^
    }

    public void GameOverScreen()
    {
        StartCoroutine(GameMenu("GAME OVER", delayTime));
    }

    IEnumerator GameMenu(string titleText, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        // TODO: Game over screen animation
        gameMenu.SetActive(true);
        menuTitle.SetText(titleText);
        scoreText.SetText("Your score: " + GameManager.Instance.score);
        highScoreText.SetText("High score: " + GameManager.Instance.highScore);
    }
}
