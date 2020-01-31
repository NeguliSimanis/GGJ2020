using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject spawnPoint;
    void Start()
    {
        spawnPoint = GameObject.Find("PlayerSpawn");
        Instantiate(playerPrefab, spawnPoint.transform);
    }
}
