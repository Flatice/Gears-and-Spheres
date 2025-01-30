using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : BallBehaviour // INHERITANCE
{
    [SerializeField]
    GameObject explosion;

    protected override void EnteredBasket(GameObject basket)
    {
        GameManager.Instance.GameOver();
        Explode(new Vector2(basket.transform.position.x, transform.position.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            Explode(transform.position);
        }
    }

    private void Explode(Vector2 explosionLocation)
    {
        Instantiate(explosion, explosionLocation, explosion.transform.rotation);
        Destroy(gameObject);
    }
}
