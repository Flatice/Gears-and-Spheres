using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    float boundX = 10.5f, boundY = 7.5f;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // destroy object if out of bounds
        if (Mathf.Abs(transform.position.x) > boundX || Mathf.Abs(transform.position.y) > boundY)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnteredBasket(collision.gameObject.tag); // ABSTRACTION
    }

    protected virtual void EnteredBasket(string tag)
    {
        if (gameObject.tag == tag)
        {
            GameManager.instance.UpdateScore(1);
        }
        else
        {
            Debug.Log("Wrong basket!");
        }
    }
}
