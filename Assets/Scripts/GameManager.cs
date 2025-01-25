using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }  // this object is a singleton

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreIncrease = 1)
    {
        score += scoreIncrease;
        scoreText.SetText("Score: " + score);
    }
}
