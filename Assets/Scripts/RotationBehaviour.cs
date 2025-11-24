using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    public bool rotateCounterclockwise = false;
    public Quaternion initialRotation;

    [SerializeField]
    private int _nTeeth = 9;  // backing field for nTeeth
    public int nTeeth  // ENCAPSULATION
    {
        get { return _nTeeth; }
        set
        {
            if (value < 1)
                Debug.LogError("Gears can't have a number of teeth smaller than 1");
            else
                _nTeeth = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        nTeeth = _nTeeth;
    }
}
