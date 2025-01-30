using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionAnimation : MonoBehaviour
{
    [SerializeField]
    float scalingDuration = 1f, fadingDuration = 1f;

    [SerializeField]
    Sprite[] sprites;
    int spriteIndex;

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        transform.DOScale(new Vector3(0.65f, 0.65f, 0.65f), scalingDuration)
                 .SetEase(Ease.OutCubic);

        StartCoroutine(WhiteFlashes(4, scalingDuration * 0.6f));

        yield return new WaitForSeconds(scalingDuration * 0.6f);

        spriteRenderer.DOFade(0f, fadingDuration)
            .SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(fadingDuration);

        Destroy(gameObject);
    }

    IEnumerator WhiteFlashes(int nFlashes, float scalingDuration)
    {
        float flashLoopTime = scalingDuration / (float)nFlashes;
        float flashTime = flashLoopTime * 0.2f;
        float nonFlashTime = flashLoopTime - flashTime;

        for (int i = 0; i < nFlashes; i++)
        {
            ChangeSprite();
            yield return new WaitForSeconds(flashTime);

            ChangeSprite();
            yield return new WaitForSeconds(nonFlashTime);
        }
        
    }

    public void ChangeSprite()
    {
        spriteIndex = spriteIndex >= sprites.Length - 1 ? 0 : spriteIndex + 1;
        spriteRenderer.sprite = sprites[spriteIndex];
    }
}
