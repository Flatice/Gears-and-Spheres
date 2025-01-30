using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipSpriteAnimation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float animationSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("FlipSprite");
    }

    IEnumerator FlipSprite()
    {
        while (true)
        {
            spriteRenderer.flipX = true;
            yield return new WaitForSeconds(animationSpeed);

            spriteRenderer.flipY = true;
            yield return new WaitForSeconds(animationSpeed);

            spriteRenderer.flipX = false;
            yield return new WaitForSeconds(animationSpeed);

            spriteRenderer.flipY = false;
            yield return new WaitForSeconds(animationSpeed);
        }
    }
}
