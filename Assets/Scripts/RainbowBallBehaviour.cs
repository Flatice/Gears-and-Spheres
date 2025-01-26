using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowBallBehaviour : BallBehaviour
{
    SpriteRenderer spriteRenderer;
    float hue = 0f;
    [SerializeField] float colorChangeSpeed = 0.5f;

    Rigidbody2D rb;
    [SerializeField] float forceStrength = 1f;
    bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();  // ABSTRACTION        
    }

    void FixedUpdate()
    {
        if (isGrounded)
            GoOutwards(); // ABSTRACTION 
    }

    protected override void EnteredBasket(string tag)
    {
        GameManager.instance.UpdateScore(3);
    }

    private void ChangeColor()
    {
        hue += colorChangeSpeed * Time.deltaTime;
        if (hue >= 1f)
            hue -= 1f;  // so the hue value stays between 0 and 1
        //hue = Mathf.Repeat(hue, 1f);

        spriteRenderer.color = Color.HSVToRGB(hue, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void GoOutwards()
    {
        Vector2 forceDirection = new Vector2(transform.position.x >= 0 ? 1f : -1f, 0f);
        rb.AddForce(forceDirection.normalized * forceStrength, ForceMode2D.Force);
    }
}
