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
    [SerializeField]
    private Slider hpSlider;

    private GameObject playerObject = null;
    void Start()
    {
        spawnPoint = GameObject.Find("PlayerSpawn");
        playerObject = Instantiate(playerPrefab, spawnPoint.transform);
    }

    private void Update()
    {
        hpSlider.value = playerObject.GetComponent<Player>().currentHP;

    }

}
