using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

/// <summary>
/// File Name: BulletController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Oct. 4, 2019
/// Description: Controls the Bullet prefab
/// Revision History:
/// </summary>
public class BulletController : CollidableObject
{
    public Boundary boundary;
    private bool _isPastStart;

    public float verticalSpeed;
    private bool _isEnemyBullet;

    public bool IsEnemyBullet { get; set; }


    private void OnEnable()
    {
        if (_isPastStart)
        {
            SpawnBullet();
        }
    }

    public void SpawnBullet()
    {
        if (!IsEnemyBullet)
        {
            GameObject player = GameObject.Find("Player");
            if (player == null)
            {
                Reset();
            }
            else
            {
                Vector2 spawnPoint = player.transform.position;
                transform.position = spawnPoint;
                verticalSpeed = 0.08f;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Reset();
        _isPastStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            Move();
            CheckBounds();
        }
    }

    public override void Reset()
    {
        base.Reset();
        verticalSpeed = 0.0f;
        this.gameObject.SetActive(false);
    }

    void Move()
    {
        if(this.gameObject.activeSelf)
        {
            Vector2 newPosition = new Vector2(0.0f, verticalSpeed);
            Vector2 currentPosition = transform.position;

            currentPosition += newPosition;
            transform.position = currentPosition;
        }
    }

    void CheckBounds()
    {
        if (transform.position.y >= boundary.Top ||
            transform.position.y <= boundary.Bottom)
        {
            Reset();
        }
    }
}
