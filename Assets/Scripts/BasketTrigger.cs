using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketTrigger : MonoBehaviour
{
    string basketName;
    // Start is called before the first frame update
    void Start()
    {
        basketName = gameObject.name;  // either "Basket left" or "Basket right
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == collision.gameObject.tag)
        {
            GameManager.instance.UpdateScore(1);
        }
        else
        {
            Debug.Log("Wrong basket!");
        }
        
    }
}
