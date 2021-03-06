﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningScript : MonoBehaviour
{
    private ItemType lastSpawnedEnemyType = ItemType.Worm;
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject bootFish;
    [SerializeField]
    private GameObject skullFish;
    [SerializeField]
    private GameObject wormFish;

    public List<GameObject> bootFishes = new List<GameObject>();
    public List<GameObject> skullFishes = new List<GameObject>();
    public List<GameObject> wormFishes = new List<GameObject>();

    private GameObject GameManager;

    private int maxEnemies;
    public int currentEnemies;
    public float timer;

    private GameObject playerRef;

    void Start()
    {
        maxEnemies = 7;
        currentEnemies = 0;
        timer = 0f;
        GameManager = GameObject.Find("Gamemanager");
        //maxEnemies = GameManager.GetComponent<GameManager>().maxEnemy;
    }

    void Update()
    {
        if (!playerRef)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
        }

        if (timer <= 6f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            float dist = Vector2.Distance(playerRef.transform.position, transform.position);
            if(dist >= 7.5f)
            {
                Debug.Log("Player distance:" + dist);
                timer = 0;
                SpawnEnemies();
            } else {
                timer = 3;
            }
        }
    }

    public void SpawnEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentEnemies = enemies.Length;

        if (currentEnemies >= maxEnemies)
            return;

        switch (lastSpawnedEnemyType)
        {
            case ItemType.Worm:
                {
                    GameObject newEnemyPrefab = skullFish;
                    Instantiate(newEnemyPrefab, this.transform);
                    skullFishes.Add(newEnemyPrefab);
                    EnemyStats newEnemyStats = newEnemyPrefab.GetComponent<EnemyStats>();
                    newEnemyStats.enemyType = ItemType.Skull;
                    newEnemyStats.enemySpawningScript = this;
                    lastSpawnedEnemyType = ItemType.Skull;
                    break;
                }
            case ItemType.Skull:
                {
                    GameObject newEnemyPrefab = bootFish;
                    Instantiate(newEnemyPrefab, this.transform);
                    bootFishes.Add(newEnemyPrefab);
                    EnemyStats newEnemyStats = newEnemyPrefab.GetComponent<EnemyStats>();
                    newEnemyStats.enemyType = ItemType.Boot;
                    newEnemyStats.enemySpawningScript = this;
                    lastSpawnedEnemyType = ItemType.Boot;
                    break;
                }
            case ItemType.Boot:
                {
                    GameObject newEnemyPrefab = wormFish;
                    Instantiate(newEnemyPrefab, this.transform);
                    wormFishes.Add(newEnemyPrefab);
                    EnemyStats newEnemyStats = newEnemyPrefab.GetComponent<EnemyStats>();
                    newEnemyStats.enemyType = ItemType.Worm;
                    newEnemyStats.enemySpawningScript = this;
                    lastSpawnedEnemyType = ItemType.Worm;
                }
                break;
        }
        /*
        if (bootFishes.Count < 1)
        {
            GameObject newEnemyPrefab = bootFish;
            Instantiate(newEnemyPrefab, this.transform);
            bootFishes.Add(newEnemyPrefab);
            EnemyStats newEnemyStats = newEnemyPrefab.GetComponent<EnemyStats>();
            newEnemyStats.enemyType = ItemType.Boot;
            newEnemyStats.enemySpawningScript = this;
            Debug.Log("1");
        }
        else if (skullFishes.Count < 1)
        {
            GameObject newEnemyPrefab = skullFish;
            Instantiate(newEnemyPrefab, this.transform);
            skullFishes.Add(newEnemyPrefab);
            EnemyStats newEnemyStats = newEnemyPrefab.GetComponent<EnemyStats>();
            newEnemyStats.enemyType = ItemType.Skull;
            newEnemyStats.enemySpawningScript = this;
            Debug.Log("2");
        }
        else // worm
        {
            GameObject newEnemyPrefab = wormFish;
            Instantiate(newEnemyPrefab, this.transform);
            wormFishes.Add(newEnemyPrefab);
            EnemyStats newEnemyStats = newEnemyPrefab.GetComponent<EnemyStats>();
            newEnemyStats.enemyType = ItemType.Worm;
            newEnemyStats.enemySpawningScript = this;
            Debug.Log("3");
        }*/
        

    }
}
