using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningScript : MonoBehaviour
{
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

    public int maxEnemies;
    public int currentEnemies;
    public float timer;

    void Start()
    {
        maxEnemies = 8;
        currentEnemies = 0;
        timer = 0f;
        GameManager = GameObject.Find("Gamemanager");
        maxEnemies = GameManager.GetComponent<GameManager>().maxEnemy;
    }
    
    void Update()
    {
        if (timer <= 6f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            SpawnEnemies();
        }
    }

    public void SpawnEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentEnemies = enemies.Length;

        bool hasSkullEnemy = false; //EnemyGreener
        bool hasBootEnemy = false; // Enemy 
        bool hasWormEnemy = false; // EnemyGreen

        if (currentEnemies >= maxEnemies)
            return;

        if (bootFishes.Count < 1)
        {
            enemyPrefab = bootFish;
            Instantiate(enemyPrefab, this.transform);
            bootFishes.Add(enemyPrefab);
            EnemyStats newEnemyStats = enemyPrefab.GetComponent<EnemyStats>();
            newEnemyStats.enemyType = ItemType.Boot;
            newEnemyStats.enemySpawningScript = this;
        }
        else if (skullFishes.Count < 1)
        {
            enemyPrefab = skullFish;
            Instantiate(enemyPrefab, this.transform);
            skullFishes.Add(enemyPrefab);
            EnemyStats newEnemyStats = enemyPrefab.GetComponent<EnemyStats>();
            newEnemyStats.enemyType = ItemType.Skull;
            newEnemyStats.enemySpawningScript = this;
        }
        else // worm
        {
            enemyPrefab = wormFish;
            Instantiate(enemyPrefab, this.transform);
            wormFishes.Add(enemyPrefab);
            EnemyStats newEnemyStats = enemyPrefab.GetComponent<EnemyStats>();
            newEnemyStats.enemyType = ItemType.Worm;
            newEnemyStats.enemySpawningScript = this;
        }
        

    }
}
