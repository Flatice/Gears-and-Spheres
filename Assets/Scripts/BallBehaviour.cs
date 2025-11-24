using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    float boundX = 10.5f, boundY = 7.5f;


    // Update is called once per frame
    void Update()
    {
        // destroy object if out of bounds
        if (Mathf.Abs(transform.position.x) > boundX || Mathf.Abs(transform.position.y) > boundY)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.gameOver)
            EnteredBasket(collision.gameObject); // ABSTRACTION
    }

    protected virtual void EnteredBasket(GameObject basket)
    {
        if (gameObject.tag == basket.tag)
        {
            GameManager.Instance.UpdateScore(1);
        }
        else
        {
            Debug.Log("Wrong basket!");
        }
    }
}
