using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : BallBehaviour // INHERITANCE
{

    protected override void EnteredBasket(string tag)
    {
        Debug.Log("BOOM!");  // TODO: explosion animation
        GameManager.instance.GameOver();
    }


}
