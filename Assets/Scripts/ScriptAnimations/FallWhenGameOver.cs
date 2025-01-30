using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWhenGameOver : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    float boundX = 20f, boundY = 20f;
    float timeBeforeFall = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        rb.isKinematic = true;
        if (circleCollider != null)
            circleCollider.enabled = false;

        timeBeforeFall = Random.Range(timeBeforeFall - 0.16f, timeBeforeFall + 0.16f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            if (rb.isKinematic)
            {
                Invoke(nameof(Fall), timeBeforeFall);
            }
            
            // destroy object if it falls too far away to prevent falling indefinitely
            if (Mathf.Abs(transform.position.x) > boundX || Mathf.Abs(transform.position.y) > boundY)
                Destroy(gameObject);
        }
    }

    private void Fall()
    {
        rb.isKinematic = false;
        if (circleCollider != null)
            circleCollider.enabled = true;
    }
}
