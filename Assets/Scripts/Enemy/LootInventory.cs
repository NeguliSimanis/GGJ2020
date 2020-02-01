using UnityEngine;

public class LootInventory : MonoBehaviour
{

    public GameObject[] inventory;

    GameObject getLoot()
    {
        int i = Random.Range(0, inventory.Length);
        GameObject item = inventory[i];
        return item;
    }

    public void SpawnItem()
    {
        Vector2 spawnPOS = new Vector2(transform.position.x, transform.position.y);
        Instantiate(getLoot(), spawnPOS, Quaternion.identity);
    }
}
