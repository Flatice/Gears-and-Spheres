using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject gameOverScreen, startMenu, pauseMenu;
    Button buttonStartGame; 

    [SerializeField]
    float gameOverDelayTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen = transform.Find("Game Over Screen").gameObject;
        startMenu = transform.Find("Menu Start").gameObject;

        buttonStartGame = startMenu.transform.Find("Button start game").gameObject.GetComponent<Button>();
        // FIXME: useless here I think, use OnPointerDown in a script attached to the Button
        
        // all menus exists at startup, they are activated and deactivated wheen needed
        gameOverScreen.SetActive(false);
        // Menu Start is already active at startup

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
