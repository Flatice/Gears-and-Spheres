using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // ENCAPSULATION

    public int score = 0, highScore = 0;
    public bool gameOver = false, isPaused = true;

    UIManager uiManager;
    TMP_Text scoreText;
    GameObject floor;
    Camera mainCamera;

    string savePath;

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

        savePath = Application.persistentDataPath + "/savefile.json";
        LoadGame();
    }

    // since GameManager is a sigleton and persists between scenes, I added this method that executes every time the scene is loaded
    // the method is called by SceneController.Start()
    public void InitializeScene()
    {
        uiManager = UIManager.Instance;
        floor = GameObject.Find("Floor");
        scoreText = GameObject.Find("Text Score").GetComponent<TMP_Text>();
        mainCamera = Camera.main;

        // reset game state
        gameOver = false;
        isPaused = true;
        Time.timeScale = 1;

        // reset score
        score = 0;
        UpdateScore(0);

        floor.SetActive(false);
    }

    public void UpdateScore(int scoreIncrease = 1)
    {
        score += scoreIncrease;
        scoreText.SetText("Score: " + score);

        if (score > highScore)
            highScore = score;
    }

    public void StartGame()
    {
        uiManager.StartGameAnimation();  // ABSTRACTION
        isPaused = false;
    }

    public void PauseResumeGame()
    {        
        if (!gameOver)
        {
            if (isPaused)   // resume game
            {
                isPaused = false;
                uiManager.HideGameMenu();  // ABSTRACTION
                Time.timeScale = 1;
            }
            else            // pause game
            {
                isPaused = true;
                uiManager.PauseMenu();  // ABSTRACTION
                Time.timeScale = 0;
            }
            
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            mainCamera.transform.DOShakePosition(0.5f, 0.35f);
            gameOver = true;
            floor.SetActive(true);
            uiManager.GameOverScreen();  // ABSTRACTION
            SaveGame();
        }
    }

    public void QuitGame()
    {
        SaveGame();

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
        if (Input.GetKeyDown(KeyCode.Escape)) PauseResumeGame();
        // just for debugging purposes:
        // if (Input.GetKeyDown(KeyCode.Space)) GameOver();
    }


    class SaveData
    {
        public int highScore;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
        }
    }
}
