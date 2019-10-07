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

    //Private instance variables
    private AudioSource _ding;
    private AudioSource _explosion;
    void Start()
    {
        GameObject gameControllerObject = GameObject.Find("GameController");
        if(gameControllerObject == null)
        {
            Debug.Log("Can't find Game Controller");
        }
        gameController = gameControllerObject.GetComponent<GameController>();
        _ding = gameController.audioSources[(int)SoundClip.DING];
        _explosion = gameController.audioSources[(int)SoundClip.EXPLOSION];
        _ding.volume = 0.5f;
        _explosion.volume = 0.5f;
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
                        _explosion.Play();
                        CollidableObject enemy = other.gameObject.GetComponent<CollidableObject>();
                        gameController.Lives -= 1;
                        enemy.HasCollided = true;
                        player.HasCollided = true;
                    }
                    break;
                // Handles Player and Asteroid collision
                case "Asteroid":
                    AsteroidController asteroid = other.gameObject.GetComponent<AsteroidController>();
                    if (!asteroid.HasCollided)
                    {
                        _ding.Play();
                        gameController.Score += 100;
                        asteroid.HasCollided = true;
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
                    _explosion.Play();
                    CollidableObject enemy = other.gameObject.GetComponent<CollidableObject>();
                    CollidableObject bullet = this.gameObject.GetComponent<CollidableObject>();
                    enemy.Reset();
                    bullet.Reset();
                    break;
            }
        }
    }
}
