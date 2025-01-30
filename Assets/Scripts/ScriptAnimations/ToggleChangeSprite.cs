using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleChangeSprite : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    Image currentSprite;
    int spriteIndex = 0;

    // I do it on OnEnable so I'm sure that it gets executed every time I reactivate the object and 
    // because otherwise I would split the logic between OnEnable and Start risking a Null reference
    private void OnEnable()
    {
        currentSprite = GetComponent<Image>();
        spriteIndex = 0;
        currentSprite.sprite = sprites[0];
    }

    public void ChangeSprite()
    {
        spriteIndex = spriteIndex >= sprites.Length - 1 ? 0 : spriteIndex + 1;
        currentSprite.sprite = sprites[spriteIndex];
    }
}
