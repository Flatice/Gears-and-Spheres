using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }  // ENCAPSULATION

    public int score = 0;
    public bool gameOver = false;

    TMP_Text scoreText;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Text Score").GetComponent<TMP_Text>();
    }

    public void UpdateScore(int scoreIncrease = 1)
    {
        score += scoreIncrease;
        scoreText.SetText("Score: " + score);
    }

    public void GameOver()
    {
        gameOver = true;
        Debug.LogError("Game Over");
    }
}
