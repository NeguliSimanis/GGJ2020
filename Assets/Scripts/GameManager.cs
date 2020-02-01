using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private GameObject spawnPoint;


    private GameObject playerObject = null;
    [SerializeField]
    private GameObject itemSpawnPoint = null;

    private GameObject itemObject = null;

    public GameObject[] itemPrefabs;

    public int maxEnemy;

    void Start()
    {
        spawnPoint = GameObject.Find("PlayerSpawn");
        playerObject = Instantiate(playerPrefab, spawnPoint.transform);
        itemObject = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)],itemSpawnPoint.transform);
        maxEnemy = 5;

    }

    private void Update()
    {

    }

}
