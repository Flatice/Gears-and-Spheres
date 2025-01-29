using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : BallBehaviour // INHERITANCE
{

    protected override void EnteredBasket(string tag)
    {
        Debug.Log("BOOM!");  // TODO: explosion animation
        GameManager.Instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            // TODO: add animation
        }
    }


}
