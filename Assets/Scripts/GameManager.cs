using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // ENCAPSULATION

    UIManager uiManager;

    public int score = 0;
    public bool gameOver = false;
    GameObject floor;

    TMP_Text scoreText;

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

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        floor = GameObject.Find("Floor");
        scoreText = GameObject.Find("Text Score").GetComponent<TMP_Text>();

        floor.SetActive(false);
    }

    public void UpdateScore(int scoreIncrease = 1)
    {
        score += scoreIncrease;
        scoreText.SetText("Score: " + score);
    }

    public void GameOver()
    {
        gameOver = true;
        floor.SetActive(true);
        uiManager.GameOverScreen();  // ABSTRACTION
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    private void Update()
    {
        // just as temporary debug (TODO: deleate)
        if (Input.GetKeyDown(KeyCode.Escape)) GameOver();
    }
}
