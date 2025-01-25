using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    
    GameObject[] rotatingObjects;

    Bounds bounds;

    public float rotation = 0;
    float rotationLimit = 30;

    // Start is called before the first frame update
    void Start()
    {
        rotatingObjects = GameObject.FindGameObjectsWithTag("Rotating");

        // The player can control the rotation of the platforms by dragging the mouse inside the area of this GameObject so I need the bonuds of its Collider
        bounds = GetComponent<BoxCollider2D>().bounds;  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            ChangeRotation();
    }
    
    private void ChangeRotation()
    {
        // Converting the screen position (pixels) to the world position makes sure the mouse position coordinates are within the range of the bounds
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = 0f;  // ScreenToWorldPoint expects a Z coordinate for the mouse. In an orthograhpic 2D game, the camera position is 0
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        
        // Normalizing the position to be within the limit angles of rotation
        rotation = MousePositionNormalized(mouseWorldPosition.y, -rotationLimit, rotationLimit);

        foreach (GameObject obj in rotatingObjects)
        {            
            if (obj != null)
            {
                if (rotation <= rotationLimit && rotation >= -rotationLimit)
                {
                    RotationBehaviour objRotationData = obj.GetComponent<RotationBehaviour>();
                    // if the object should rotate counterclockwise I multiply the rotation by -1
                    int direction = objRotationData.rotateCounterclockwise ? -1 : 1;
                    // by multiplying the rotation with the gear ratio I get the correct rotational speed of gears with different teeth numbers. Formula: Rotation_2 / Rotation_1 = nTeeth_1 / nTeeth_2
                    float scaledRotation = 9f / objRotationData.nTeeth * rotation;

                    // final rotation = rotation at the start of the game + rotation applied by the player
                    // This takes into account objects that has been rotated to visually fit into position
                    // A sum between two angles represented in quaternions is done by multiplying the quaternions
                    obj.transform.rotation = objRotationData.initialRotation * Quaternion.Euler(0, 0, scaledRotation * direction);
                }
                    
            }
        }
    }

    // Min-max feature scaling (or rescaling):
    private float MousePositionNormalized(float pos, float minRange, float maxRange)
    {
        float topY = bounds.max.y, bottomY = bounds.min.y;

        return minRange + (pos - bottomY)*(maxRange - minRange) / (topY - bottomY);

    }
}
