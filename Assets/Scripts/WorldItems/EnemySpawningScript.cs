using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    public int maxEnemies;
    public int currentEnemies;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        maxEnemies = 3;
        currentEnemies = 0;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //check current enemy count
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentEnemies = enemies.Length;
        //spawn enemy after 5 seconds, until get to max number of enemies;


        if (timer <= 5f)
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
        if (currentEnemies < maxEnemies)
        {
            Instantiate(enemyPrefab, this.transform);
        }

    }
}
