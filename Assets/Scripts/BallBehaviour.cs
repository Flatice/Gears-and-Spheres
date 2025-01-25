using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float spawnForceX;

    float boundX = 10.5f, boundY = 7.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float direction = transform.position.x < 0 ? 1 : -1;
        Vector2 spawnForce = new Vector2(spawnForceX * direction, 0);

        rb.AddForce(spawnForce, ForceMode2D.Impulse);
        // TODO: I set spawnForceX to 0, deleate this code if it won't be used
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x) > boundX || Mathf.Abs(transform.position.y) > boundY)
            Destroy(gameObject);
    }
}
