using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BobLeftRight : MonoBehaviour
{
    [SerializeField]
    float movementLength = 10f, movementDuration = 1f;

    RectTransform rectTransform;
    Tween movementAnimation;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartAnimation();
    }

    void StartAnimation()
    {
        movementAnimation = rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + movementLength, movementDuration)
                                         .SetLoops(-1, LoopType.Yoyo)
                                         .SetEase(Ease.InOutSine);

        // TODO: NEEDS DOcomplete
    }

    // I kill the animation so DOTween doesn't continue the animation even when the object is deactivated
    void OnDisable()
    {
        if (movementAnimation != null && movementAnimation.IsActive())
            movementAnimation.Kill();
    }

    // renable the animation oh reload
    void OnEnable()
    {
        if (movementAnimation != null && !movementAnimation.IsActive())
            StartAnimation();
    }



}
