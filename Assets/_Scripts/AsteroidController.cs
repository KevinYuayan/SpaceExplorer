using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

/// <summary>
/// File Name: AsteroidController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Oct. 2, 2019
/// Description: Controller for the Asteroid prefab
/// Revision History:
/// Renamed to AsteroidController.
/// </summary>
public class AsteroidController : CollidableObject
{
    public float verticalSpeed = 0.05f;
    public Material[] materialsArray;

    public Boundary boundary;

    private Renderer renderer;

    public override bool HasCollided
    {
        get
        {
            return base.HasCollided;
        }
        set
        {
            base.HasCollided = value;
            if(value)
            {
                renderer.sharedMaterial = materialsArray[(int)Materials.DEPLETED];
            }
        }
    }
    private enum Materials
    {
        NONE = -1,
        RICH,
        DEPLETED,
        NUM_OF_MATERIALS
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    /// <summary>
    /// This method moves the Asteroid down the screen by verticalSpeed
    /// </summary>
    void Move()
    {
        Vector2 newPosition = new Vector2(0.0f, verticalSpeed);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }

    /// <summary>
    /// This method resets the Asteroid to the resetPosition
    /// </summary>
    public override void Reset()
    {
        base.Reset();
        renderer.sharedMaterial = materialsArray[(int)Materials.RICH];
        float randomXPosition = Random.Range(boundary.Left, boundary.Right);
        transform.position = new Vector2(randomXPosition, boundary.Top);
    }

    /// <summary>
    /// This method checks if the Asteroid reaches the lower boundary
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
