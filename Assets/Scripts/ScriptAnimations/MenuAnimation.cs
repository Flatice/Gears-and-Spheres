using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuAnimation : MonoBehaviour
{
    float fadeInTime = 0.5f;

    CanvasGroup menu;

    // Start is called before the first frame update
    void Awake()
    {
        menu = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            fadeInTime = GameManager.Instance.gameOver ? 0.5f : 0.1f;

        menu.alpha = 0f;
        menu.DOFade(1, fadeInTime).SetUpdate(true);  // without SetUpdate(true) it won't run while the game is paused because Time.timescale = 0
    }
}

