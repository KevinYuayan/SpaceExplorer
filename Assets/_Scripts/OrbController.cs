using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

/// <summary>
/// File Name: OrbController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Oct. 2, 2019
/// Description: Controller for the Orb prefab
/// Revision History:
/// </summary>
public class OrbController : CollidableObject
{
    public float verticalSpeed = 0.05f;


    public Boundary boundary;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    /// <summary>
    /// This method moves the ocean down the screen by verticalSpeed
    /// </summary>
    void Move()
    {
        Vector2 newPosition = new Vector2(0.0f, verticalSpeed);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }

    /// <summary>
    /// This method resets the ocean to the resetPosition
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        float randomXPosition = Random.Range(boundary.Left, boundary.Right);
        transform.position = new Vector2(randomXPosition, boundary.Top);
    }

    /// <summary>
    /// This method checks if the ocean reaches the lower boundary
    /// and then it Resets it
    /// </summary>
    void CheckBounds()
    {
        if (transform.position.y <= boundary.Bottom)
        {
            Reset();
        }
    }
}
