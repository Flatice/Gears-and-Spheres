using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleChangeSprite : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    Image currentSprite;
    public int spriteIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentSprite = GetComponent<Image>();
        currentSprite.sprite = sprites[0];
    }

    private void OnEnable()
    {
        Debug.Log("test");
        spriteIndex = 0;
        currentSprite.sprite = sprites[0];
    }

    public void ChangeSprite()
    {
        spriteIndex = spriteIndex >= sprites.Length - 1 ? 0 : spriteIndex + 1;
        currentSprite.sprite = sprites[spriteIndex];
    }
}
