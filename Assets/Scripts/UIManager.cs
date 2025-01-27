using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject gameOverScreen, startMenu, pauseMenu;

    [SerializeField]
    float gameOverDelayTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = transform.Find("Game Over Screen").gameObject;

        // all menus exists at startup, they are activated and deactivated wheen needed
        gameOverScreen.SetActive(false);

    }

    public void GameOverScreen()
    {
        Invoke("GameOverAnimation", gameOverDelayTime);
    }

    private void GameOverAnimation()
    {
        // TODO: Game over screen animation
        gameOverScreen.SetActive(true);
    }
}
