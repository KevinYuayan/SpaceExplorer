using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// File Name: BulletManager.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Oct. 4, 2019
/// Description: Creates and spawns bullets
/// Revision History:
/// </summary>
public class BulletManager : MonoBehaviour
{
    // Iterator used so Shoot is only called once every second at most
    private int _nextShot;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
        _nextShot = (int)Time.time;
    }

    /// <summary>
    /// Activates the next bullet in queue while space is held
    /// </summary>
    /// <param name="tag">Bullet</param>
    public void Shoot(string tag)
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Fire Bullet");
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            BulletController bullet = objectToSpawn.GetComponent<BulletController>();
            
            bullet.IsEnemyBullet = false;
            objectToSpawn.SetActive(true);

            poolDictionary[tag].Enqueue(objectToSpawn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _nextShot)
        {
            Shoot("Bullet");
            _nextShot += 1;
        }
    }
}
