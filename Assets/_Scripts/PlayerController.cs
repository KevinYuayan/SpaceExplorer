using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
/// <summary>
/// File Name: PlayerController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Oct. 2, 2019
/// Description: Controller for the Player prefab
/// Revision History:
/// </summary>
public class PlayerController : CollidableObject
{
    public Speed speed;
    public Boundary boundary;

    public GameController gameController;

    // private instance variables
    private int _invincibilityTime = 2;
    private float _invincibleOpacity = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    public override bool HasCollided
    {
        get
        {
            return _hasCollided;
        }
        set
        {
            _hasCollided = value;
            if (value == true)
            {
                StartCoroutine(ITime());
            }
        }
    }
    /// <summary>
    /// Method used to control the player's invulnerability time
    /// </summary>
    /// <returns>Wait for seconds 2</returns>
    private IEnumerator ITime ()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, _invincibleOpacity);
        Debug.Log("Invincible");
        yield return new WaitForSeconds(_invincibilityTime);
        Debug.Log("before Invincible turned off");
        HasCollided = false;
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    /// <summary>
    /// Player can move in all directions
    /// </summary>
    public void Move()
    {
        Vector2 newPosition = transform.position;

        if(Input.GetAxis("Horizontal") > 0.0f)
        {
            newPosition += new Vector2(speed.max, 0.0f);
        }

        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            newPosition += new Vector2(speed.min, 0.0f);
        }

        if (Input.GetAxis("Vertical") < 0.0f)
        {
            newPosition += new Vector2(0.0f, speed.min);
        }

        if (Input.GetAxis("Vertical") > 0.0f)
        {
            newPosition += new Vector2(0.0f, speed.max);
        }
        transform.position = newPosition;
    }

    /// <summary>
    /// Keeps the player in the playable area
    /// </summary>
    public void CheckBounds()
    {
        // check right boundary
        if (transform.position.x > boundary.Right)
        {
            transform.position = new Vector2(boundary.Right, transform.position.y);
        }

        // check left boundary
        if (transform.position.x < boundary.Left)
        {
            transform.position = new Vector2(boundary.Left, transform.position.y);
        }

        // check bottom boundary
        if (transform.position.y < boundary.Bottom)
        {
            transform.position = new Vector2(transform.position.x, boundary.Bottom);
        }

        // check top boundary
        if (transform.position.y > boundary.Top)
        {
            transform.position = new Vector2(transform.position.x, boundary.Top);
        }
    }
}
