using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// File Name: CollisionController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Oct. 2, 2019
/// Description: Handles Collisions in the game
/// Revision History:
/// </summary>
public class CollisionController : MonoBehaviour
{
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.Find("GameController");
        if(gameControllerObject == null)
        {
            Debug.Log("Can't find Game Controller");
        }
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.tag == "Player")
        {
            switch (other.gameObject.tag)
            {
                // Handles Player and Enemy collision
                case "Enemy_1":
                case "Enemy_2":
                    CollidableObject player = this.gameObject.GetComponent<CollidableObject>();
                    if (!player.HasCollided)
                    {
                        CollidableObject enemy = other.gameObject.GetComponent<CollidableObject>();
                        gameController.Lives -= 1;
                        enemy.HasCollided = true;
                        player.HasCollided = true;
                    }
                    break;
                // Handles Player and Orb collision
                case "Orb":
                    CollidableObject orb = other.gameObject.GetComponent<CollidableObject>();
                    if (!orb.HasCollided)
                    {
                        gameController.Score += 100;
                        orb.HasCollided = true;
                    }
                    break;
            }
        }
        else if (this.gameObject.tag == "Bullet")
        {
            switch (other.gameObject.tag)
            {
                // Handles Bullet and Enemy collision
                case "Enemy_1":
                case "Enemy_2":
                    CollidableObject enemy = other.gameObject.GetComponent<CollidableObject>();
                    CollidableObject bullet = this.gameObject.GetComponent<CollidableObject>();
                    enemy.Reset();
                    bullet.Reset();
                    break;
            }
        }
    }
}
