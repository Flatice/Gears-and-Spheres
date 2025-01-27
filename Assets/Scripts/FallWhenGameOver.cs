using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWhenGameOver : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider != null)
            circleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            rb.isKinematic = false;
            if (circleCollider != null)
                circleCollider.enabled = true;
        }
    }
}
