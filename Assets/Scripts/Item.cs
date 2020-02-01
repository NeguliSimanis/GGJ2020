using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Skull,
    Boot,
    Worm
}
public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().hasItem = true;
            PickupItem();
        }
    }

    private void PickupItem()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Destroy(this.gameObject);
    }

}
