using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// File Name: SpaceController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Oct. 2, 2019
/// Description: Controller for the space prefab (background)
/// Revision History:
/// </summary>
public class SpaceController : MonoBehaviour
{
    public float verticalSpeed;
    public float resetPosition;
    public float resetPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        Move();
    }

    /// <summary>
    /// This method moves the space background down the screen by verticalSpeed
    /// </summary>
    void Move()
    {
        Vector2 newPosition = new Vector2(0.0f, verticalSpeed);
        Vector2 currentPosition = transform.position;
        
        transform.position = currentPosition - newPosition;
    }
    /// <summary>
    /// This method resets the ocean to the resetPosition
    /// </summary>
    void Reset()
    {
        transform.position = new Vector2(0.0f, resetPosition);
    }

    /// <summary>
    /// This method checks if the ocean reaches the lower boundary
    /// and then it Resets it
    /// </summary>
    void CheckBounds()
    {
        if(transform.position.y <= resetPoint)
        {
            Reset();
        }
    }
}
