using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float _myTime;
    private PlayerController player;

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
        if(GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
    }



    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            player.Shoot("Bullet", this);
        }
    }
}
